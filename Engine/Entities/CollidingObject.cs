using Microsoft.Xna.Framework;
using MonoGameJam4.Engine.WorldSpace;

namespace MonoGameJam4.Engine.Entities
{
    public class CollidingObject : GameObject
    {
        public CollidingObject(GameCenter gameCenter, Transform transform, string name) : base(gameCenter, transform,
            name)
        {
        }

        public bool CheckCollision(Transform a, Transform b)
        {
            float ppu = GameCenter.Camera.PixelsPerUnit;
            
            Rectangle rectA = new Rectangle((GetOriginPoint(a) * ppu).ToPoint(), (a.Scale * ppu).ToPoint());
            Rectangle rectB = new Rectangle((GetOriginPoint(b) * ppu).ToPoint(), (b.Scale * ppu).ToPoint());

            return rectA.Intersects(rectB);
        }

        private static Vector2 GetOriginPoint(Transform transform)
        {
            return new Vector2(GetOriginPosition(transform.Position.X, transform.Scale.X),
                GetOriginPosition(transform.Position.Y, transform.Scale.Y));
        }

        private static float GetOriginPosition(float position, float scale)
        {
            return position - scale / 2;
        }
    }
}