using Microsoft.Xna.Framework;
using MonoGameJam4.Engine.Interfaces;
using MonoGameJam4.Engine.WorldSpace;
using IUpdateable = MonoGameJam4.Engine.Interfaces.IUpdateable;

namespace MonoGameJam4.Engine.Entities
{
    public class GameObject : IUpdateable, IPositionable
    {
        /// <summary> represents the world position of the object. should only be modified throughout the derived classes methods </summary>
        public Transform Transform { get; set; }
        public string Name { get; set; }

        public GameCenter GameCenter;

        public GameObject(GameCenter gameCenter, Transform transform, string name)
        {
            GameCenter = gameCenter;
            Transform = transform;
            Name = name;
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Deconstruct()
        {
            
        }
    }
}