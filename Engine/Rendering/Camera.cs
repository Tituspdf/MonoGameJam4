using Microsoft.Xna.Framework;
using MonoGameJam4.Engine.Entities;
using MonoGameJam4.Engine.WorldSpace;

namespace MonoGameJam4.Engine.Rendering
{
    public class Camera : GameObject
    {
        public float Zoom = 1;
        public int PixelsPerUnit = 50;
        private Window _window;
        
        public Camera(GameCenter gameCenter, Transform transform, string name, Window window) : base(gameCenter, transform, name)
        {
            _window = window;
        }

        public Vector2 ScreenToWorldPosition(Vector2 pos)
        {
            pos = _window.ScreenMiddlePoint - pos;
            pos /= PixelsPerUnit;
            pos.X *= -1f;
            return pos + Transform.Position;
        }

        public float ScreenToWorldDistance(float f)
        {
            return f / PixelsPerUnit;
        }
    }
}