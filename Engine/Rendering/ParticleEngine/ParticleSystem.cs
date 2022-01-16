using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameJam4.Engine.Debugging;
using MonoGameJam4.Engine.Entities;
using MonoGameJam4.Engine.Interfaces;
using MonoGameJam4.Engine.Mathematics;
using MonoGameJam4.Engine.WorldSpace;
using Random = System.Random;

namespace MonoGameJam4.Engine.Rendering.ParticleEngine
{
    public struct ParticleData
    {
        private enum GetMode { Random, Static,}
        
        public int Amount;
        public Color Color;
        public readonly Texture2D Texture;
        public readonly float LifeTime;
        public Vector2 Size;
        
        // velocity variables
        private readonly GetMode _velocityGetMode;
        private readonly float _velocityStatic;
        private readonly RandomFloat _velocityRandom;

        public readonly float Velocity
        {
            get
            {
                return _velocityGetMode switch
                {
                    GetMode.Random => _velocityRandom.Number,
                    GetMode.Static => _velocityStatic,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }

        public ParticleData(int amount, Color color, Texture2D texture, float lifeTime, Vector2 size, float velocity)
        {
            Amount = amount;
            Color = color;
            Texture = texture;
            LifeTime = lifeTime;
            Size = size;
            
            _velocityGetMode = GetMode.Static;
            _velocityStatic = velocity;
            _velocityRandom = new RandomFloat(); // it must have a default value
        }
        
        public ParticleData(int amount, Color color, Texture2D texture, float lifeTime, Vector2 size, RandomFloat velocity)
        {
            Amount = amount;
            Color = color;
            Texture = texture;
            LifeTime = lifeTime;
            Size = size;
            
            _velocityGetMode = GetMode.Random;
            _velocityRandom = velocity;
            _velocityStatic = 0;
        }
    }

    /// <summary>
    /// particle Object
    /// based on http://rbwhitaker.wikidot.com/2d-particle-engine-1
    /// </summary>
    public class ParticleSystem : GameObject, IRenderCall
    {
        private readonly Random _random;

        /// <summary> the list which keeps track of all current particles </summary>
        private List<Particle> _particles;

        private readonly ParticleData _data;

        public ParticleSystem(GameCenter gameCenter, Transform transform, string name, ParticleData data) : base(
            gameCenter, transform, name)
        {
            _data = data;
            _particles = new List<Particle>();
            _random = new Random();

            int total = 10;

            for (int i = 0; i < total; i++)
            {
                _particles.Add(GenerateNewParticle());
            }
        }

        private Particle GenerateNewParticle()
        {
            Vector2 position = Transform.Position;
            Vector2 velocity = new Vector2(
                Mathematics.Random.RandomFloat(-_data.Velocity, _data.Velocity),
                Mathematics.Random.RandomFloat(-_data.Velocity, _data.Velocity));
            float angle = 0;

            return new Particle(new Transform(position, _data.Size, angle), _data.Texture, velocity,
                _data.Color);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            for (int particle = 0; particle < _particles.Count; particle++)
            {
                _particles[particle].Update();
                if (_particles[particle].LiveTime >= _data.LifeTime)
                {
                    _particles.RemoveAt(particle);
                    particle--;
                }
            }
            
            if (_particles.Count == 0)
                Deconstruct();
        }

        public void Render(SpriteBatch spriteBatch, Camera camera, Window gameWindow)
        {
            foreach (Particle t in _particles)
            {
                t.Render(spriteBatch, camera, gameWindow);
            }
        }
    }
}