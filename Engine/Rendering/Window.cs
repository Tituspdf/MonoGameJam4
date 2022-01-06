using Microsoft.Xna.Framework;

namespace MonoGameJam4.Engine.Rendering
{
    public class Window : EngineObject
    {
        private GraphicsDeviceManager _graphics;

        public Vector2 ScreenSize { get; private set; }

        public Window(GraphicsDeviceManager graphics)
        {
            _graphics = graphics;
            ScreenSize = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
        }
    }
}