namespace MonoGameJam4.Engine.Mathematics
{
    public readonly struct RandomFloat
    {
        private readonly float _min;
        private readonly float _max;

        public float Number => Random.RandomFloat(_min, _max);

        public RandomFloat(float min, float max)
        {
            _min = min;
            _max = max;
        }
    }
}