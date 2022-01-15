using System;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoGameJam4.Engine;
using MonoGameJam4.Engine.Debugging;
using MonoGameJam4.Engine.Entities;
using MonoGameJam4.Engine.Interfaces;
using MonoGameJam4.Engine.Rendering;
using MonoGameJam4.Engine.WorldSpace;
using MonoGameJam4.GameContent.Interfaces;

namespace MonoGameJam4.GameContent.Entities
{
    public class Enemy : WorldObject, IRendering, IHittable
    {
        private GameObject _player;
        public Renderer Renderer { get; set; }

        private float _speed = 0.7f;

        private const float BulletDamage = 20;
        private const float MaxHealth = 50;
        private float _health;

        public Enemy(GameCenter gameCenter, Transform transform, string name, bool colliding) : base(gameCenter, transform, name, colliding)
        {
            Renderer = new Renderer(this, "Point");
            foreach (GameObject obj in gameCenter.GameObjects.Where(obj => obj.Name == "Player"))
            {
                _player = obj;
                break;
            }

            _health = MaxHealth;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Vector2 movement = _player.Transform.Position - Transform.Position;
            movement *= Time.DeltaTime * _speed;
            MoveX(movement.X, OnCollision);
            MoveY(movement.Y, OnCollision);
        }

        private void OnCollision(CollidingObject obj)
        {
            
        }

        public void Hit()
        {
            _health -= BulletDamage;
            Renderer.Color = Color.White * (_health / MaxHealth);
            
            if (_health < 0) Deconstruct();
        }

        public override void Deconstruct()
        {
            base.Deconstruct();
            Renderer.Deconstruct();
        }
    }
}