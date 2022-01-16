using System;
using Microsoft.Xna.Framework;
using MonoGameJam4.Engine;
using MonoGameJam4.Engine.Debugging;
using MonoGameJam4.Engine.Entities;
using MonoGameJam4.Engine.Input;
using MonoGameJam4.Engine.Interfaces;
using MonoGameJam4.Engine.Rendering;
using MonoGameJam4.Engine.WorldSpace;

namespace MonoGameJam4.GameContent.Entities
{
    public class Player : WorldObject, IRendering
    {
        public Renderer Renderer { get; set; }
        private InputManagement _input;
        private Vector2 _normalizedLookDirection;
        private float _bulletOffset = 1f;
        private float _bulletVelocity = 5;

        private const float MaxHealth = 100;
        private float _currentHealth;
        private const float EnemyDamage = 15;
        
        public Player(GameCenter gameCenter, Transform transform, string name) : base(gameCenter, transform, name, true)
        {
            Renderer = new Renderer(this, "Player");
            _input = gameCenter.InputManagement;
            _input.GetCallbackMouse(MouseElement.LeftButton).Invoked += OnMouse;

            _currentHealth = MaxHealth;
        }

        private void OnMouse()
        {
            GameCenter.GameObjects.Add(new Bullet(GameCenter, new Transform(Transform.Position + _normalizedLookDirection * _bulletOffset, Bullet.Size, 0), "bullet", true, _normalizedLookDirection * _bulletVelocity));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Vector2 move = _input.Movement * Time.DeltaTime * 3.5f;
            
            MoveX(move.X, delegate { });
            MoveY(move.Y, delegate { });

            Vector2 lookDir = GameCenter.Camera.ScreenToWorldPosition(_input.MousePosition) - Transform.Position;
            float angle = (float) Math.Atan2(-lookDir.Y, lookDir.X) + MathHelper.ToRadians(90); 
            Transform.Rotation = angle;

            _normalizedLookDirection = lookDir;
            _normalizedLookDirection.Normalize();
        }

        public void EnemyHit()
        {
            ChangeHealth(-EnemyDamage);
        }

        private void ChangeHealth(float value)
        {
            _currentHealth += value;

            if (_currentHealth < 0)
            {
                Debug.Log("Death");
            }
        }
    }
}