using MonoGameJam4.Engine.Entities;
using MonoGameJam4.Engine.WorldSpace;

namespace MonoGameJam4.GameContent.Entities
{
    public class EnemySpawner : GameObject
    {
        public EnemySpawner(GameCenter gameCenter, Transform transform, string name) : base(gameCenter, transform, name)
        {
        }
    }
}