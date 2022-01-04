using Microsoft.Xna.Framework;
using MonoGameJam4.Engine.Interfaces;
using MonoGameJam4.Engine.WorldSpace;
using IUpdateable = MonoGameJam4.Engine.Interfaces.IUpdateable;

namespace MonoGameJam4.Engine
{
    public class GameObject : IUpdateable, IPositionable
    {
        public Transform Transform { get; set; }
        public string Name { get; set; }

        public GameObject(Transform transform, string name)
        {
            Transform = transform;
            Name = name;
        }

        public virtual void Update(GameTime gameTime)
        {
        }
    }
}