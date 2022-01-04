using Microsoft.Xna.Framework;

namespace MonoGameJam4.Engine
{
    public class Time : EngineObject
    {
        public static float Scale { get; set; }
        public static float DeltaTime { get; set; }
        public static float FixedDeltaTime { get; set; }

        public Time()
        {
            Scale = 1;
        }
        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            FixedDeltaTime = gameTime.ElapsedGameTime.Seconds;
            DeltaTime = FixedDeltaTime * Scale;
        }
    }
}