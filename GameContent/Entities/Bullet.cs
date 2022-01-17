using Microsoft.Xna.Framework;
using MonoGameJam4.Engine;
using MonoGameJam4.Engine.Debugging;
using MonoGameJam4.Engine.Entities;
using MonoGameJam4.Engine.Interfaces;
using MonoGameJam4.Engine.Mathematics;
using MonoGameJam4.Engine.Rendering;
using MonoGameJam4.Engine.Rendering.ParticleEngine;
using MonoGameJam4.Engine.WorldSpace;
using MonoGameJam4.GameContent.Interfaces;

namespace MonoGameJam4.GameContent.Entities
{
    public class Bullet : WorldObject, IRendering, IResettable
    {
        public static readonly Vector2 Size = new Vector2(1) * 0.3f;

        private Vector2 _velocity;

        public Renderer Renderer { get; set; }

        public Bullet(GameCenter gameCenter, Transform transform, string name, bool colliding, Vector2 velocity) : base(
            gameCenter, transform, name, colliding)
        {
            _velocity = velocity;
            Renderer = new Renderer(this, "Point");
            gameCenter.ContentLoader.Sounds["Shoot"].Play();
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
            (obj as IHittable)?.Hit();
            Deconstruct();
        }

        public override void Deconstruct()
        {
            base.Deconstruct();
            Renderer.Deconstruct();
            ParticleData data = new ParticleData(30, Color.White, GameCenter.ContentLoader.Textures["Point"], new RandomFloat(0.78f, 1.245f),
                new Vector2(0.1f), new RandomFloat(0.2f, 0.6f));
            GameCenter.GameObjects.Add(new ParticleSystem(GameCenter,
                new Transform(Transform.Position, Transform.Scale, 0), "BulletParticle", data));
        }

        public void Reset()
        {
            Deconstruct();
        }
    }
}