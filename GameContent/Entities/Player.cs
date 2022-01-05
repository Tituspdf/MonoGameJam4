using MonoGameJam4.Engine;
using MonoGameJam4.Engine.Interfaces;
using MonoGameJam4.Engine.Rendering;
using MonoGameJam4.Engine.WorldSpace;

namespace MonoGameJam4.GameContent.Entities
{
    public class Player : GameObject, IRendering
    {
        public Renderer Renderer { get; set; }
        
        public Player(GameCenter gameCenter, Transform transform, string name) : base(gameCenter, transform, name)
        {
            Renderer = new Renderer(this, "Player");
        }
    }
}