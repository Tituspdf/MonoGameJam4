namespace MonoGameJam4.Engine
{
    public class Component
    {
        protected GameObject GameObject;

        protected Component(GameObject gameObject)
        {
            GameObject = gameObject;
        }
    }
}