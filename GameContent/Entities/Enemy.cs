using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using MonoGameJam4.Engine;
using MonoGameJam4.Engine.Debugging;
using MonoGameJam4.Engine.Entities;
using MonoGameJam4.Engine.Interfaces;
using MonoGameJam4.Engine.Mathematics;
using MonoGameJam4.Engine.Rendering;
using MonoGameJam4.Engine.Rendering.ParticleEngine;
using MonoGameJam4.Engine.WorldSpace;
using MonoGameJam4.GameContent.Interfaces;
using Random = MonoGameJam4.Engine.Mathematics.Random;

namespace MonoGameJam4.GameContent.Entities
{
    public class Enemy : WorldObject, IRendering, IHittable
    {
        private enum BehaviorState
        {
            Spawning,
            Attack
        }

        private BehaviorState _state;

        private GameObject _player;
        public Renderer Renderer { get; set; }

        private float _speed = 2.5f;

        private const float BulletDamage = 20;
        private const float MaxHealth = 40;
        private float _health;

        private const float GrowingRate = 1.05f;
        
        private readonly float _minX;
        private readonly float _minY;
        private readonly float _maxX;
        private readonly float _maxY;
        
        private float _playerDistance = 3f;
        private SoundEffect _sound;

        public Enemy(GameCenter gameCenter, Transform transform, string name, bool colliding) : base(gameCenter,
            transform, name, colliding)
        {
            Renderer = new Renderer(this, "Point");
            foreach (GameObject obj in gameCenter.GameObjects.Where(obj => obj.Name == "Player"))
            {
                _player = obj;
                break;
            }

            _health = MaxHealth;

            _state = BehaviorState.Spawning;
            
            Vector2 tl = gameCenter.Camera.ScreenToWorldPosition(new Vector2(0, 0));
            Vector2 br = gameCenter.Camera.ScreenToWorldPosition(gameCenter.GameWindow.ScreenSize);
            _minX = tl.X + 1;
            _maxX = br.X - 1;
            _minY = br.Y + 1;
            _maxY = tl.Y - 1;
            
            Vector2 position;
            float distance;
            
            do
            {
                position = new Vector2(Random.RandomFloat(_minX, _maxX), Random.RandomFloat(_minY, _maxY));
                distance = (position - _player.Transform.Position).Length();
            } while (distance <= _playerDistance && CheckOtherEnemies(position));

            transform.Position = position;

            _sound = gameCenter.ContentLoader.Sounds["EnemyExplode"];
        }

        private bool CheckOtherEnemies(Vector2 position)
        {
            foreach (var worldObject in GameCenter.GetColliders().Where(o => o is Enemy))
            {
                Enemy enemy = (Enemy) worldObject;
                Transform.Position = position;
                if (CheckCollision(Transform, enemy.Transform)) return false;
            }

            return true;
        }
        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            switch (_state)
            {
                case BehaviorState.Spawning:
                {
                    Transform.Scale *= GrowingRate;

                    if (Transform.Scale.X > 1f)
                        _state = BehaviorState.Attack;
                    break;
                }
                case BehaviorState.Attack:
                {
                    Vector2 movement = _player.Transform.Position - Transform.Position;
                    movement.Normalize();
                    movement *= Time.DeltaTime * _speed;
                    MoveX(movement.X, OnCollision);
                    MoveY(movement.Y, OnCollision);
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnCollision(CollidingObject obj)
        {
            if (!(obj is Player player)) return;
            player.EnemyHit();
            Deconstruct();
        }

        public void Hit()
        {
            _health -= BulletDamage;
            Renderer.Color = Color.White * (_health / MaxHealth);

            if (_health <= 0)
            {
                _sound.Play();
                Deconstruct();
                (_player as Player)!.AddScore(1);
            }
        }

        public override void Deconstruct()
        {
            Renderer.Deconstruct();
            ParticleData data = new ParticleData(50, Color.White, GameCenter.ContentLoader.Textures["Point"],
                new RandomFloat(0.75f, 1.35f), new Vector2(0.3f), new RandomFloat(0.6f, 1.24f));
            GameCenter.GameObjects.Add(new ParticleSystem(GameCenter,
                new Transform(Transform.Position, Transform.Scale, 0), "EnemyDestroyParticle", data));
            
            base.Deconstruct();
        }
    }
}