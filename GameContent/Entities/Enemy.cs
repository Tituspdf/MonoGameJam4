using MonoGameJam4.Engine.Entities;
using MonoGameJam4.Engine.WorldSpace;

namespace MonoGameJam4.GameContent.Entities
{
    public class Enemy : Actor
    {
        public Enemy(GameCenter gameCenter, Transform transform, string name, bool colliding) : base(gameCenter, transform, name, colliding)
        {
        }
    }
}