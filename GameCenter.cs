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
        private SpriteBatch _spriteBatch;

        private EngineObject[] _engineClasses;
        private InputManagement _inputManagement;

        private List<GameObject> _gameObjects;
        private Camera _camera;

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
            _gameObjects = new List<GameObject>();

            _camera = new Camera(new Transform(Vector2.Zero, Vector2.Zero), "Camera");
            _gameObjects.Add(_camera);

            _inputManagement.GetCallback(Keys.Space).Invoked += () => { Debug.LogError("Hello World"); };

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
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

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}