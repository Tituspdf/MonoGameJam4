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
    }
}