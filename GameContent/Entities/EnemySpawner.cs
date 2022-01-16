using Microsoft.Xna.Framework;
using MonoGameJam4.Engine;
using MonoGameJam4.Engine.Entities;
using MonoGameJam4.Engine.Mathematics;
using MonoGameJam4.Engine.WorldSpace;

namespace MonoGameJam4.GameContent.Entities
{
    public class EnemySpawner : GameObject
    {
        private float _spawnDelay = 5f;
        private float _playerDistance = 4f;
        private float _spawnTimer;

        private readonly float _minX;
        private readonly float _minY;
        private readonly float _maxX;
        private readonly float _maxY;

        private Player _player;

        public EnemySpawner(GameCenter gameCenter, Transform transform, string name, Player player) : base(gameCenter, transform, name)
        {
            _player = player;
            _spawnTimer = _spawnDelay;

            Vector2 tl = gameCenter.Camera.ScreenToWorldPosition(new Vector2(0, 0));
            Vector2 br = gameCenter.Camera.ScreenToWorldPosition(gameCenter.GameWindow.ScreenSize);
            _minX = tl.X + 1;
            _maxX = br.X - 1;
            _minY = br.Y + 1;
            _maxY = tl.Y - 1;
            
            Spawn();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _spawnTimer -= Time.DeltaTime;
            if (_spawnTimer < 0)
            {
                Spawn();
                _spawnTimer = _spawnDelay;
            }
        }

        private void Spawn()
        {
            Vector2 position;
            float distance;
            do
            {
                position = new Vector2(Random.RandomFloat(_minX, _maxX), Random.RandomFloat(_minY, _maxY));
                distance = (position - _player.Transform.Position).Length();
            } while (distance <= _playerDistance);

            GameCenter.GameObjects.Add(new Enemy(GameCenter, new Transform(position, Vector2.One, 0), "Enemy",
                true));
        }
    }
}