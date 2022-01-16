using System.Linq;
using Microsoft.Xna.Framework;
using MonoGameJam4.Engine;
using MonoGameJam4.Engine.Entities;
using MonoGameJam4.Engine.WorldSpace;
using MonoGameJam4.GameContent.Interfaces;

namespace MonoGameJam4.GameContent.Entities
{
    public class EnemySpawner : GameObject, IResettable
    {
        private float _spawnDelay = 5f;
        private float _spawnTimer;

        private Player _player;

        private int _wave;
        private int _enemiesPerWave = 1;

        public EnemySpawner(GameCenter gameCenter, Transform transform, string name, Player player) : base(gameCenter,
            transform, name)
        {
            _player = player;
            _spawnTimer = _spawnDelay;
            _wave = 0;
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
            for (int i = 0; i < _enemiesPerWave; i++)
            {
                GameCenter.GameObjects.Add(new Enemy(GameCenter, new Transform(Vector2.Zero, Vector2.One * 0.1f, 0),
                    "Enemy",
                    true));
            }

            _wave++;

            if (_wave % 4 == 0) _enemiesPerWave++;
        }

        public void Reset()
        {
            foreach (GameObject gameObject in GameCenter.GameObjects.ToArray().Where(o => o is Enemy))
            {
                Enemy enemy = (Enemy) gameObject;
                enemy.Deconstruct();
            }

            _wave = 0;
            _enemiesPerWave = 0;
        }
    }
}