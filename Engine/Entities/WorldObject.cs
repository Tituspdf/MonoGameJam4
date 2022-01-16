using System;
using Microsoft.Xna.Framework;
using MonoGameJam4.Engine.Debugging;
using MonoGameJam4.Engine.Enums;
using MonoGameJam4.Engine.WorldSpace;

namespace MonoGameJam4.Engine.Entities
{
    public class WorldObject : CollidingObject
    {
        public bool Colliding;

        public WorldObject(GameCenter gameCenter, Transform transform, string name, bool colliding) : base(gameCenter,
            transform, name)
        {
            Colliding = colliding;
        }

        public void MoveX(float amount, Action<CollidingObject> onCollision)
        {
            if (amount == 0) return;

            Vector2 move = new Vector2(amount, 0);

            foreach (GameObject o in GameCenter.GetColliders())
            {
                WorldObject worldObject = (WorldObject) o;
                if (worldObject == this) continue;
                if (!CheckCollision(Transform.Moved(move), worldObject.Transform)) continue;
                onCollision?.Invoke(worldObject);
                return;
            }

            Transform.Position += move;
        }

        public void MoveY(float amount, Action<CollidingObject> onCollision)
        {
            if (amount == 0) return;

            Vector2 move = new Vector2(0, amount);

            foreach (GameObject o in GameCenter.GetColliders())
            {
                WorldObject worldObject = (WorldObject) o;
                if (worldObject == this) continue;
                if (!CheckCollision(Transform.Moved(move), worldObject.Transform)) continue;
                onCollision?.Invoke(worldObject);
                return;
            }

            Transform.Position += move;
        }
    }
}