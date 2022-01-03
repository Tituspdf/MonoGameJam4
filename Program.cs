using System;

namespace MonoGameJam4
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new GameCenter())
                game.Run();
        }
    }
}
