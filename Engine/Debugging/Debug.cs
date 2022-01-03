using System;

namespace MonoGameJam4.Engine.Debugging
{
    public static class Debug
    {
        private static readonly Action<object> Logging = Console.WriteLine;
        
        public static void Log(object o)
        {
            Logging.Invoke(o);
        }

        public static void LogWarning(object o)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Logging.Invoke(o);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void LogError(object o)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Logging.Invoke(o);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}