using Microsoft.Xna.Framework;
using MonoGameJam4.Engine;
using MonoGameJam4.Engine.Debugging;
using MonoGameJam4.Engine.Entities;
using MonoGameJam4.Engine.Interfaces;
using MonoGameJam4.Engine.Rendering;
using MonoGameJam4.Engine.WorldSpace;

namespace MonoGameJam4.GameContent.Entities
{
    public class Bullet : WorldObject, IRendering
    {
        public static readonly Vector2 Size = new Vector2(1) * 0.3f;

        private Vector2 _velocity;
        
        public Renderer Renderer { get; set; }
        
        public Bullet(GameCenter gameCenter, Transform transform, string name, bool colliding, Vector2 velocity) : base(gameCenter, transform, name, colliding)
        {
            _velocity = velocity;
            Renderer = new Renderer(this, "Point");
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
            Deconstruct();
        }

        public override void Deconstruct()
        {
            base.Deconstruct();
            Renderer.Deconstruct();
        }
    }
}