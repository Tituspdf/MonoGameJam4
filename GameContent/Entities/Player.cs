using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGameJam4.Engine;
using MonoGameJam4.Engine.Entities;
using MonoGameJam4.Engine.Input;
using MonoGameJam4.Engine.Interfaces;
using MonoGameJam4.Engine.Mathematics;
using MonoGameJam4.Engine.Rendering;
using MonoGameJam4.Engine.Rendering.ParticleEngine;
using MonoGameJam4.Engine.WorldSpace;
using MonoGameJam4.GameContent.Interfaces;
using MonoGameJam4.GameContent.UI;

namespace MonoGameJam4.GameContent.Entities
{
    public class Player : WorldObject, IRendering, IResettable
    {
        public enum PlayerState
        {
            Pause,
            /// <summary> state where the player can move freely </summary>
            Fighting,

            /// <summary> state where the game is waiting for the upgrade </summary>
            Upgrading,

            /// <summary> state where the update animation is playing </summary>
            Waiting,
        }

        public PlayerState State;
        private const float UpgradeTime = 2f;
        private float _upgradeTimer;

        public Renderer Renderer { get; set; }
        private InputManagement _input;
        private Vector2 _normalizedLookDirection;
        private float _bulletOffset = 1f;
        private float _bulletVelocity = 5;

        private const float MaxHealth = 100;
        private float _currentHealth;
        private const float EnemyDamage = 15;
        private float HealthRegeneration = 0.05f;

        /// <summary> the amount of health which is left (value between 0 and 1 </summary>
        public float HealthProgress => _currentHealth / MaxHealth;

        public int MaxBullets = 5;
        public int CurrentBullets;
        private float _bulletTimer;
        private float BulletTime = 1.5f;

        public int Score { get; private set; }
        public int CurrentLevel;
        public int NextLevel;

        private readonly Screens _screens;
        
        public Player(GameCenter gameCenter, Transform transform, string name, Screens screens) : base(gameCenter, transform, name, true)
        {
            _screens = screens;
            Renderer = new Renderer(this, "Player");
            _input = gameCenter.InputManagement;
            _input.GetCallbackMouse(MouseElement.LeftButton).Invoked += OnMouse;
            _input.GetCallback(Keys.Space).Invoked += OnUpgradeButton;

            State = PlayerState.Fighting;

            _currentHealth = MaxHealth;

            CurrentBullets = MaxBullets;
            _bulletTimer = BulletTime;

            Score = 0;

            CurrentLevel = 0;
            NextLevel = 1;
        }

        private void OnUpgradeButton()
        {
            if (State != PlayerState.Upgrading) return;

            State = PlayerState.Waiting;
            _upgradeTimer = UpgradeTime;
        }

        private void OnMouse()
        {
            if (State != PlayerState.Fighting || CurrentBullets == 0) return;

            GameCenter.GameObjects.Add(new Bullet(GameCenter,
                new Transform(Transform.Position + _normalizedLookDirection * _bulletOffset, Bullet.Size, 0), "bullet",
                true, _normalizedLookDirection * _bulletVelocity));

            CurrentBullets -= 1;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            switch (State)
            {
                case PlayerState.Fighting:
                {
                    Vector2 move = _input.Movement * Time.DeltaTime * 3.5f;

                    MoveX(move.X, delegate { });
                    MoveY(move.Y, delegate { });

                    Vector2 lookDir = GameCenter.Camera.ScreenToWorldPosition(_input.MousePosition) -
                                      Transform.Position;
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

                    break;
                }
                case PlayerState.Upgrading:
                    break;
                case PlayerState.Waiting:
                {
                    Transform.Rotation += 20f * Time.FixedDeltaTime;
                    _upgradeTimer -= Time.FixedDeltaTime;
                    if (_upgradeTimer <= 0) LevelUp();
                    break;
                }
                case PlayerState.Pause:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
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
                OnDeath();
            }

            if (_currentHealth > MaxHealth)
            {
                _currentHealth = MaxHealth;
            }
        }

        public void AddScore(int amount)
        {
            Score += amount;

            if (Score % 5 != 0) return;
            Time.Scale = 0;
            State = PlayerState.Upgrading;
        }

        private void LevelUp()
        {
            State = PlayerState.Fighting;
            Time.Scale = 1;
            ParticleData data = new ParticleData(25, Color.White,
                GameCenter.ContentLoader.Textures["EmptyPoint"], new RandomFloat(1f, 1.8f), new Vector2(0.3f),
                new RandomFloat(1.4f, 1.9f));
            GameCenter.GameObjects.Add(new ParticleSystem(GameCenter,
                new Transform(Transform.Position, Transform.Scale, Transform.Rotation), "PlayerUpgradeParticle", data));

            CurrentLevel += 1;

            HealthRegeneration *= 1.2f;
            HealthRegeneration = MathHelper.Clamp(HealthRegeneration, 0, 0.9f);

            BulletTime *= 0.8f;
            BulletTime = MathHelper.Clamp(BulletTime, 0.2f, 10);

            _bulletVelocity *= 1.1f;
            _bulletVelocity = MathHelper.Clamp(_bulletVelocity, 0, 10f);

            if (Score % 10 != 0) return;
            MaxBullets++;
            MaxBullets = MathHelper.Clamp(MaxBullets, 5, 10);
        }

        private void OnDeath()
        {
            foreach (GameObject gameObject in GameCenter.GameObjects.ToArray().Where(o => o is IResettable))
            {
                (gameObject as IResettable)?.Reset();
            }
        }

        public void Reset()
        {
            HealthRegeneration = 0.05f;
            MaxBullets = 5;
            BulletTime = 1.5f;
            CurrentBullets = MaxBullets;
            _bulletVelocity = 5;
            CurrentLevel = 0;
            _currentHealth = MaxHealth;
            Score = 0;
            Time.Scale = 0;
            _screens.State = Screens.GameState.Death;
        }
    }
}