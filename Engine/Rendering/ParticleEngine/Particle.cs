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

        public void Render(SpriteBatch spriteBatch, Camera camera, Window window)
        {
            Vector2 size = Transform.Scale * camera.PixelsPerUnit;

            // find out the position relative to the camera
            Vector2 relativePosition = Transform.Position - camera.Transform.Position;
            // convert the position into pixel space
            relativePosition *= camera.Zoom * camera.PixelsPerUnit;
            // fix direction
            relativePosition.Y *= -1;
            // align the position relative to the screen center
            relativePosition += window.ScreenMiddlePoint;

            Vector2 origin = _texture.Bounds.Center.ToVector2();

            // hint: the renderer takes the rotation as degrees
            float rotation = Transform.Rotation;

            Rectangle destinationRectangle = new Rectangle((int) (relativePosition.X), (int) (relativePosition.Y),
                (int) size.X, (int) size.Y);

            spriteBatch.Draw(_texture, destinationRectangle, null, _color, rotation, origin, SpriteEffects.None, 1);
        }
    }
}