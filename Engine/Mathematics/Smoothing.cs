using MonoGameJam4.Engine.Debugging;

namespace MonoGameJam4.Engine.Mathematics
{
    public struct Smoothing
    {
        public static float Linear(float current, float destiny, float modifier)
        {
            if (current >= destiny)
            {
                return destiny;
            }
            if (current + modifier <= destiny)
            {
                return current + modifier;
            }
            if (current + modifier >= destiny)
            {
                return destiny;
            }
            Debug.LogError("unexpected linear smoothing behaving");
            return current;
        }
    }
}