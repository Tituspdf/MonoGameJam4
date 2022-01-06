using Microsoft.Xna.Framework;

namespace MonoGameJam4.Engine
{
    public class Time : EngineObject
    {
        public static float Scale { get; set; }
        public static float DeltaTime { get; private set; }
        public static float FixedDeltaTime { get; private set; }

        public Time()
        {
            Scale = 1;
        }
        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            FixedDeltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds;
            DeltaTime = FixedDeltaTime * Scale;
        }
    }
}