using System;
using System.Diagnostics;

namespace Sokoban
{
    internal static class Program
    {
        private static int Main(string[] args)
        {
            var game = new ConsoleGame();
            game.Start("Level0.txt");

            if (Debugger.IsAttached)
            {
                Console.WriteLine("Press any key to exit . . .");
                Console.ReadKey();
            }

            return 0;
        }
    }
}
