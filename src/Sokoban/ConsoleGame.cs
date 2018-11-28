using System;
using System.Linq;

namespace Sokoban
{
    public class ConsoleGame
    {
        public void Start(string levelName)
        {
            var level = LevelParser.FromFile(levelName);
            Print(level);

            while (true)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Escape:
                        return;

                    case ConsoleKey.R:
                        level = LevelParser.FromFile(levelName);
                        break;

                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        level.MovePlayer(Direction.North);
                        break;
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.D:
                        level.MovePlayer(Direction.East);
                        break;
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        level.MovePlayer(Direction.South);
                        break;
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.A:
                        level.MovePlayer(Direction.West);
                        break;
                }

                Print(level);

                if (level.HasWon())
                {
                    Console.WriteLine("\nCongratulations you've solved the puzzle!\n");
                    return;
                }
            }
        }

        private static void Print(Level level)
        {
            Console.Clear();
            for (var y = 0; y < level.Height; y++)
            {
                for (var x = 0; x < level.Width; x++)
                {
                    Console.Write(GetCharAtPoint(new Point(x, y)));
                }

                Console.WriteLine();
            }

            char GetCharAtPoint(Point point)
            {
                if (level.Walls.Contains(point))
                {
                    return LevelParser.WallTile;
                }

                var isTargetTile = level.TargetPositions.Contains(point);
                if (level.PlayerPosition == point)
                {
                    return isTargetTile
                        ? LevelParser.PlayerOnTarget
                        : LevelParser.Player;
                }

                if (level.BoxPositions.Any(b => b == point))
                {
                    return isTargetTile
                        ? LevelParser.BoxOnTarget
                        : LevelParser.Box;
                }

                return isTargetTile
                    ? LevelParser.TargetTile
                    : LevelParser.EmptyTile;
            }
        }
    }
}
