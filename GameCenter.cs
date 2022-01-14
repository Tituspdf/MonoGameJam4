using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameJam4.Engine;
using MonoGameJam4.Engine.Debugging;
using MonoGameJam4.Engine.Entities;
using MonoGameJam4.Engine.Enums;
using MonoGameJam4.Engine.Input;
using MonoGameJam4.Engine.Interfaces;
using MonoGameJam4.Engine.Rendering;
using MonoGameJam4.Engine.Rendering.ParticleEngine;
using MonoGameJam4.Engine.WorldSpace;
using MonoGameJam4.GameContent;
using MonoGameJam4.GameContent.Entities;
using IUpdateable = MonoGameJam4.Engine.Interfaces.IUpdateable;

namespace MonoGameJam4
{
    public class GameCenter : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch SpriteBatch;
        public event Action Rendered;

        private readonly EngineObject[] _engineClasses;
        public InputManagement InputManagement { get; private set; }
        public ContentLoader ContentLoader;
        public Window GameWindow { get; private set; }
        public Camera Camera;

        public List<GameObject> GameObjects;

        private bool _hasStarted;

        public GameCenter()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            InputManagement = new InputManagement();
            GameWindow = new Window(_graphics);

            _engineClasses = new EngineObject[]
            {
                InputManagement,
                GameWindow,
                new Time()
            };
        }

        protected override void Initialize()
        {
            ContentLoader = new ContentLoader();

            GameObjects = new List<GameObject>();

            Camera = new Camera(this, new Transform(Vector2.Zero, Vector2.Zero, 0), "Camera", GameWindow);
            GameObjects.Add(Camera);

            InputManagement.GetCallback(Keys.Space).Invoked += () => { Debug.LogError("Hello World"); };

            base.Initialize();
        }

        private void Start()
        {
            GameObjects.Add(new Player(this, new Transform(Vector2.Zero, Vector2.One, 0), "Player"));
            // GameObjects.Add(new Box(this, new Transform(new Vector2(2, 2), Vector2.One, 0), "box", true, "Square"));
            GameObjects.Add(new WorldBorder(this, new Transform(), "Bounds"));
            // GameObjects.Add(new Enemy(this, new Transform(new Vector2(2, 2), Vector2.One, 0), "Enemy", true));
            GameObjects.Add(new ParticleSystem(this, new Transform(), "ParticleSystem", ContentLoader.Textures["Point"],
                Color.White));
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            ContentLoader.LoadContend(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (!_hasStarted)
            {
                Start();
                _hasStarted = true;
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (IUpdateable element in _engineClasses)
            {
                element.Update(gameTime);
            }

            foreach (GameObject gameObject in GameObjects.ToArray())
            {
                gameObject.Update(gameTime);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            SpriteBatch.Begin();
            Rendered?.Invoke();
            // SpriteBatch.Draw(Content.Load<Texture2D>("Square"), new Rectangle(GameWindow.ScreenMiddlePoint.ToPoint(), (Vector2.One * 2).ToPoint()), Color.Green);
            foreach (GameObject obj in GameObjects)
            {
                IRenderCall call = obj as IRenderCall;
                call?.Render(SpriteBatch, Camera, GameWindow);
            }

            SpriteBatch.End();
            base.Draw(gameTime);
        }

        public GameObject[] GetColliders(EntityType type)
        {
            switch (type)
            {
                case EntityType.Actor:
                {
                    List<WorldObject> actors = new List<WorldObject>();
                    foreach (GameObject gameObject in GameObjects)
                    {
                        if (gameObject is WorldObject {Colliding: true} actor) actors.Add(actor);
                    }

                    return actors.ToArray();
                }
                case EntityType.Solid:
                    throw new NotImplementedException("Solid collision is not implemented yet");
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}