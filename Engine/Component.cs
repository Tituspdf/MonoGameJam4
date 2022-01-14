using MonoGameJam4.Engine.Entities;

namespace MonoGameJam4.Engine
{
    public class Component
    {
        protected GameObject GameObject;
        public bool Enabled;

        protected Component(GameObject gameObject, bool enabled = true)
        {
            GameObject = gameObject;
            Enabled = enabled;
        }

        public virtual void Deconstruct()
        {
            
        }
    }
}