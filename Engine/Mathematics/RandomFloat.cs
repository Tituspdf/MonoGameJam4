namespace MonoGameJam4.Engine.Mathematics
{
    public struct RandomFloat
    {
        private float _min;
        private float _max;

        public float Number => Random.RandomFloat(_min, _max);

        public RandomFloat(float min, float max)
        {
            _min = min;
            _max = max;
        }
    }
}