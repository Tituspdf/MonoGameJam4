using Microsoft.Xna.Framework;
using MonoGameJam4.Engine.Entities;
using MonoGameJam4.Engine.WorldSpace;

namespace MonoGameJam4.Engine.Rendering
{
    public class Camera : GameObject
    {
        public float Zoom = 1;
        public int PixelsPerUnit = 50;
        
        public Camera(GameCenter gameCenter, Transform transform, string name) : base(gameCenter, transform, name)
        {
            
        }

        public Vector2 ScreenToWorldPosition()
        {
            Vector2 mousePosition = GameCenter.InputManagement.MousePosition;
            mousePosition /= PixelsPerUnit;
            return mousePosition + Transform.Position;
        }
    }
}