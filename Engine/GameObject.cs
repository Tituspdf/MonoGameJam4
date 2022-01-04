using Microsoft.Xna.Framework;
using MonoGameJam4.Engine.WorldSpace;
using IUpdateable = MonoGameJam4.Engine.Interfaces.IUpdateable;

namespace MonoGameJam4.Engine
{
    public class GameObject : IUpdateable
    {
        protected Transform Transform;

        public GameObject(Transform transform)
        {
            Transform = transform;
        }

        public virtual void Update(GameTime gameTime)
        {
            
        }
    }
}