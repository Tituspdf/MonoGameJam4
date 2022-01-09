using MonoGameJam4.Engine.WorldSpace;

namespace MonoGameJam4.Engine.Entities
{
    public class CollidingObject : GameObject
    {
        public CollidingObject(GameCenter gameCenter, Transform transform, string name) : base(gameCenter, transform, name)
        {
        }
    }
}