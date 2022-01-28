using System;

namespace PlaneGAME.DesktopGL
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
