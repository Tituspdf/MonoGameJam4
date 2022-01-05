using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameJam4.Engine;
using MonoGameJam4.Engine.Debugging;
using MonoGameJam4.Engine.Input;
using MonoGameJam4.Engine.Rendering;
using MonoGameJam4.Engine.WorldSpace;
using IUpdateable = MonoGameJam4.Engine.Interfaces.IUpdateable;

namespace MonoGameJam4
{
    public class GameCenter : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch SpriteBatch;
        public event Action Rendered;

        private EngineObject[] _engineClasses;
        private InputManagement _inputManagement;
        public ContentLoader ContentLoader;

        private List<GameObject> _gameObjects;
        public Camera Camera;

        public GameCenter()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
            _inputManagement = new InputManagement();

            _engineClasses = new EngineObject[]
            {
                _inputManagement,
                new Time()
            };
        }

        protected override void Initialize()
        {
            ContentLoader = new ContentLoader();            
            
            _gameObjects = new List<GameObject>();

            Camera = new Camera(this, new Transform(Vector2.Zero, Vector2.Zero), "Camera");
            _gameObjects.Add(Camera);

            _inputManagement.GetCallback(Keys.Space).Invoked += () => { Debug.LogError("Hello World"); };

            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            ContentLoader.LoadContend(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (IUpdateable element in _engineClasses)
            {
                element.Update(gameTime);
            }

            foreach (GameObject gameObject in _gameObjects)
            {
                gameObject.Update(gameTime);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);
            
            Rendered?.Invoke();
            
            base.Draw(gameTime);
        }
    }
}