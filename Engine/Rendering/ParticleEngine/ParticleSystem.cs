using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameJam4.Engine.Rendering.ParticleEngine
{
    /// <summary>
    /// particle Object
    /// based on http://rbwhitaker.wikidot.com/2d-particle-engine-1
    /// </summary>
    public class ParticleSystem
    {
        private readonly Random _random;
        public Vector2 EmitterLocation { get; set; }
        private List<Particle> particles;
        private List<Texture2D> textures;
        
        public ParticleSystem(List<Texture2D> textures, Vector2 location)
        {
            EmitterLocation = location;
            this.textures = textures;
            this.particles = new List<Particle>();
            _random = new Random();
        }
        
        private Particle GenerateNewParticle()
        {
            Texture2D texture = textures[_random.Next(textures.Count)];
            Vector2 position = EmitterLocation;
            Vector2 velocity = new Vector2(
                1f * (float)(_random.NextDouble() * 2 - 1),
                1f * (float)(_random.NextDouble() * 2 - 1));
            float angle = 0;
            float angularVelocity = 0.1f * (float)(_random.NextDouble() * 2 - 1);
            Color color = new Color(
                (float)_random.NextDouble(),
                (float)_random.NextDouble(),
                (float)_random.NextDouble());
            float size = (float)_random.NextDouble();
            int ttl = 20 + _random.Next(40);
 
            return new Particle(texture, position, velocity, angle, angularVelocity, color, size, ttl);
        }
        
        public void Update()
        {
            int total = 10;
 
            for (int i = 0; i < total; i++)
            {
                particles.Add(GenerateNewParticle());
            }
 
            for (int particle = 0; particle < particles.Count; particle++)
            {
                particles[particle].Update();
                if (particles[particle].TTL <= 0)
                {
                    particles.RemoveAt(particle);
                    particle--;
                }
            }
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            for (int index = 0; index < particles.Count; index++)
            {
                particles[index].Draw(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}