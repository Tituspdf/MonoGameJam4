using MonoGameJam4.Engine.Debugging;
using SystemRandom = System.Random;

namespace MonoGameJam4.Engine.Mathematics
{
    public class Random
    {
        /// <summary>
        /// function to generate a random float
        /// </summary>
        /// <param name="min">minimal value</param>
        /// <param name="max">maximal value</param>
        /// <returns>a random value</returns>
        public static float RandomFloat(float min, float max)
        {
            if (min >= max)
            {
                Debug.LogError($"The minimal value ({min}) was bigger than the maximal value {max}");
                return 0;
            }

            float range = max - min;
            SystemRandom rand = new SystemRandom();
            float select = (float) rand.NextDouble();
            return min + range * select;
        }
    }
}