using System.Linq;
using MonoGameJam4.Engine.Entities;
using MonoGameJam4.Engine.WorldSpace;

namespace MonoGameJam4.GameContent.Entities
{
    public class Enemy : Actor
    {
        private GameObject _player;
        public Enemy(GameCenter gameCenter, Transform transform, string name, bool colliding) : base(gameCenter, transform, name, colliding)
        {
            foreach (GameObject obj in gameCenter.GameObjects.Where(obj => obj.Name == "Player"))
            {
                _player = obj;
                break;
            }
        }
    }
}