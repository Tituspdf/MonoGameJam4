using Microsoft.Xna.Framework;

namespace MonoGameJam4.Engine.Rendering
{
    public class Window : EngineObject
    {
        private GraphicsDeviceManager _graphics;
        
        public Window(GraphicsDeviceManager graphics)
        {
            _graphics = graphics;
        }
    }
}