using Microsoft.Xna.Framework;

namespace MonoGameJam4.Engine.Rendering
{
    public class Window : EngineObject
    {
        private readonly GraphicsDeviceManager _graphics;

        public Vector2 ScreenSize => new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

        public Vector2 ScreenMiddlePoint => ScreenSize / 2;
        private readonly Point _startSize;

        public Window(GraphicsDeviceManager graphics, Point startSize, GameCenter gameCenter)
        {
            _graphics = graphics;
            _startSize = startSize;
            gameCenter.Window.Title = "SPACEANGLE";
        }

        public override void OnInitialize()
        {
            base.OnInitialize();
            _graphics.PreferredBackBufferWidth = _startSize.X;  // set this value to the desired width of your window
            _graphics.PreferredBackBufferHeight = _startSize.Y;   // set this value to the desired height of your window
            _graphics.ApplyChanges();
        }
    }
}