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
using MonoGameJam4.Engine.WorldSpace;
using MonoGameJam4.GameContent;
using MonoGameJam4.GameContent.Entities;
using MonoGameJam4.GameContent.UI;
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
            GameWindow = new Window(_graphics, new Point(1280, 720));

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

            foreach (EngineObject engineObject in _engineClasses)
            {
                engineObject.OnInitialize();
            }

            base.Initialize();
        }

        private void Start()
        {
            Player player = new Player(this, new Transform(Vector2.Zero, Vector2.One, 0), "Player");
            GameObjects.Add(player);
            // GameObjects.Add(new Box(this, new Transform(new Vector2(2, 2), Vector2.One, 0), "box", true, "Square"));
            GameObjects.Add(new WorldBorder(this, new Transform(), "Bounds"));
            GameObjects.Add(new EnemySpawner(this, new Transform(), "EnemySpawner", player));

            GameObjects.Add(new HealthBar(this, new Transform(), "HealthBar", player));
            GameObjects.Add(new BulletDisplay(this, new Transform(), "BulletDisplay", player));

            GameObjects.Add(new Score(this, new Transform(), "ScoreDisplay", player));
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
            
            // debug point in the middle of the screen
            // Texture2D debugTexture = Content.Load<Texture2D>("Square");
            // SpriteBatch.Draw(debugTexture,
            //     new Rectangle(GameWindow.ScreenMiddlePoint.ToPoint(), (Vector2.One * 2).ToPoint()), null, Color.Green,
            //     0, debugTexture.Bounds.Center.ToVector2(), SpriteEffects.None, 1);
            
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