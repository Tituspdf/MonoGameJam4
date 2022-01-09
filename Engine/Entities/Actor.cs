using System;
using Microsoft.Xna.Framework;
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

        private void MoveX(float amount, Action<CollidingObject> onCollision)
        {
            if (amount == 0) return;

            Vector2 move = new Vector2(amount, 0);
            
            foreach (GameObject o in GameCenter.GetColliders(EntityType.Actor))
            {
                Actor actor = (Actor) o;
                if (!CheckCollision(Transform.Moved(move), actor.Transform)) continue;
                onCollision?.Invoke(actor);
                return;
            }

            Transform.Position += move;
        }

        private void MoveY(float amount, Action<CollidingObject> onCollision)
        {
            if (amount == 0) return;

            Vector2 move = new Vector2(0, amount);
            
            foreach (GameObject o in GameCenter.GetColliders(EntityType.Actor))
            {
                Actor actor = (Actor) o;
                if (!CheckCollision(Transform.Moved(move), actor.Transform)) continue;
                onCollision?.Invoke(actor);
                return;
            }

            Transform.Position += move;
        }
    }
}