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
        private const float HealthRegeneration = 0.01f;
        /// <summary> the amount of health which is left (value between 0 and 1 </summary>
        public float HealthProgress => _currentHealth / MaxHealth;

        public const int MaxBullets = 5;
        public int CurrentBullets;
        private float _bulletTimer;
        private const float BulletTime = 1.5f;

        public int Score { get; private set; }

        public Player(GameCenter gameCenter, Transform transform, string name) : base(gameCenter, transform, name, true)
        {
            Renderer = new Renderer(this, "Player");
            _input = gameCenter.InputManagement;
            _input.GetCallbackMouse(MouseElement.LeftButton).Invoked += OnMouse;

            _currentHealth = MaxHealth;

            CurrentBullets = MaxBullets;
            _bulletTimer = BulletTime;

            Score = 0;
        }

        private void OnMouse()
        {
            if (CurrentBullets == 0) return;
            
            GameCenter.GameObjects.Add(new Bullet(GameCenter,
                new Transform(Transform.Position + _normalizedLookDirection * _bulletOffset, Bullet.Size, 0), "bullet",
                true, _normalizedLookDirection * _bulletVelocity));

            CurrentBullets -= 1;
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

            ChangeHealth(HealthRegeneration);

            // bullet regeneration 
            if (MaxBullets <= CurrentBullets) return;
            
            _bulletTimer -= Time.DeltaTime;

            if (_bulletTimer < 0)
            {
                CurrentBullets += 1;
                _bulletTimer = BulletTime;
            }
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

            if (_currentHealth > MaxHealth)
            {
                _currentHealth = MaxHealth;
            }
        }
    }
}