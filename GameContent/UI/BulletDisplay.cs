using MonoGameJam4.Engine.Entities;
using MonoGameJam4.Engine.WorldSpace;
using MonoGameJam4.GameContent.Entities;

namespace MonoGameJam4.GameContent.UI
{
    public class BulletDisplay : GameObject
    {
        private readonly Player _player;
        
        public BulletDisplay(GameCenter gameCenter, Transform transform, string name, Player player) : base(gameCenter, transform, name)
        {
            _player = player;
        }
    }
}