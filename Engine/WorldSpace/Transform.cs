using Microsoft.Xna.Framework;

namespace MonoGameJam4.Engine.WorldSpace
{
    public class Transform
    {
        public Vector2 Position { get; set; }
        public Vector2 Scale { get; set; }

        public Transform(Vector2 position, Vector2 scale)
        {
            Position = position;
            Scale = scale;
        }
                
        public Transform Moved(Vector2 amount)
        {
            return new Transform(Position + amount, Scale);
        }
    }
}