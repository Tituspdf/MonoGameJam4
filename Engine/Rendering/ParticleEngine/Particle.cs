using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameJam4.Engine.Interfaces;
using MonoGameJam4.Engine.WorldSpace;

namespace MonoGameJam4.Engine.Rendering.ParticleEngine
{
    /// <summary>
    /// particle element for <see cref="ParticleSystem"/>
    /// </summary>
    public class Particle : IPositionable
    {
        public Transform Transform { get; set; }

        private readonly Texture2D _texture; // The texture that will be drawn to represent the particle
        private Vector2 Velocity; // The speed of the particle at the current instance
        private float AngularVelocity; // The speed that the angle is changing

        /// <summary>the color of the particle</summary>
        private readonly Color _color; // The size of the particle

        public int LiveTime { get; set; } // The 'time to live' of the particle

        public Particle(Transform transform, Texture2D texture, Vector2 velocity, float angularVelocity, Color color, int liveTime)
        {
            Transform = transform;
            _texture = texture;
            Velocity = velocity;
            AngularVelocity = angularVelocity;
            _color = color;
            LiveTime = liveTime;
        }
        
        public void Update()
        {
            LiveTime--;
            Transform.Position += Velocity * Time.DeltaTime;
            Transform.Rotation += AngularVelocity * Time.DeltaTime;
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle = new Rectangle(0, 0, _texture.Width, _texture.Height);
            Vector2 origin = _texture.Bounds.Center.ToVector2();
 
            spriteBatch.Draw(_texture, Transform.Position, sourceRectangle, _color, 
                Transform.Rotation, origin, Transform.Scale, SpriteEffects.None, 0f);
        }
    }
}