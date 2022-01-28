using System;

namespace PlaneGAME.WindowsDX
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new PlaneGAMEGame())
                game.Run();
        }
    }
}
