using MonoGameJam4.Engine.Entities;
using MonoGameJam4.Engine.Rendering;
using MonoGameJam4.Engine.WorldSpace;

namespace MonoGameJam4.GameContent.Entities
{
    public class Box : WorldObject
    {
        private Renderer _renderer;
        
        public Box(GameCenter gameCenter, Transform transform, string name, bool colliding, string textureName) : base(gameCenter, transform, name, colliding)
        {
            _renderer = new Renderer(this, textureName, 1);
        }
    }
}