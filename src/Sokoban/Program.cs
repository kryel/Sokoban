using System;
using System.Diagnostics;

namespace Sokoban
{
    internal static class Program
    {
        private static int Main(string[] args)
        {
            var sokobanCli = new SokobanCli();
            sokobanCli.Start();

            if (Debugger.IsAttached)
            {
                Console.WriteLine("Press any key to exit . . .");
                Console.ReadKey();
            }

            return 0;
        }
    }
}
