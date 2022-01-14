using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameJam4.Engine.Debugging;
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

        /// <summary> sprite for the particle </summary>
        private readonly Texture2D _texture; 
        /// <summary> speed of the particle </summary>
        private readonly Vector2 _velocity; 
        /// <summary> angle changing speed </summary>
        private readonly float _angularVelocity; 

        /// <summary>the color of the particle</summary>
        private readonly Color _color; 

        public float LiveTime { get; set; } // The 'time to live' of the particle

        public Particle(Transform transform, Texture2D texture, Vector2 velocity, Color color, float liveTime)
        {
            Transform = transform;
            _texture = texture;
            _velocity = velocity;
            _color = color;
            LiveTime = liveTime;
        }

        public void Update()
        {
            LiveTime -= Time.DeltaTime;
            Transform.Position += _velocity * Time.DeltaTime;
            Transform.Rotation += _angularVelocity * Time.DeltaTime;
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