using Microsoft.Xna.Framework;

namespace MonoGameJam4.Engine.WorldSpace
{
    public class Transform
    {
        public Vector2 Position { get; set; }
        public Vector2 Scale { get; set; }
        public float Rotation { get; set; }

        public Transform(Vector2 position, Vector2 scale, float rotation)
        {
            Position = position;
            Scale = scale;
            Rotation = rotation;
        }
                
        public Transform Moved(Vector2 amount)
        {
            return new Transform(Position + amount, Scale, Rotation);
        }
    }
}