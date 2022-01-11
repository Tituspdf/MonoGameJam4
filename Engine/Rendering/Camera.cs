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

        public Vector2 ScreenToWorldPosition()
        {
            Vector2 mousePosition = GameCenter.InputManagement.MousePosition;
            
            mousePosition /= PixelsPerUnit;
            return mousePosition + Transform.Position;
        }
    }
}