using Microsoft.Xna.Framework;
using MonoGameJam4.Engine;
using MonoGameJam4.Engine.Entities;
using MonoGameJam4.Engine.WorldSpace;

namespace MonoGameJam4.GameContent.Entities
{
    public class Bullet : Actor
    {
        private Vector2 _velocity;
        public Bullet(GameCenter gameCenter, Transform transform, string name, bool colliding, Vector2 velocity) : base(gameCenter, transform, name, colliding)
        {
            _velocity = velocity;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Vector2 movement = _velocity * Time.DeltaTime;
            MoveX(movement.X, OnCollision);
            MoveY(movement.Y, OnCollision);
        }

        private void OnCollision(CollidingObject obj)
        {
            
        }
    }
}