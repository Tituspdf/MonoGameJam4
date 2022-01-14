using MonoGameJam4.Engine.Entities;

namespace MonoGameJam4.Engine
{
    public class Component
    {
        protected GameObject GameObject;

        protected Component(GameObject gameObject)
        {
            GameObject = gameObject;
        }

        public virtual void Deconstruct()
        {
            
        }
    }
}