using System;
using MonoGameJam4.Engine.Enums;
using MonoGameJam4.Engine.WorldSpace;

namespace MonoGameJam4.Engine.Entities
{
    public class Actor : CollidingObject
    {
        public bool Colliding;
        
        public Actor(GameCenter gameCenter, Transform transform, string name, bool colliding) : base(gameCenter, transform, name)
        {
            Colliding = colliding;
        }

        private void MoveX(float amount, Action onCollision)
        {
            if (amount == 0) return;

            foreach (var o in GameCenter.GetColliders(EntityType.Actor))
            {
                Actor actor = (Actor) o;
                
            }
        }

        private void MoveY(float amount, Action onCollision)
        {
            
        }
    }
}