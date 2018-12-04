using System;
using System.Linq;
using Sokoban.Implementation;

namespace Sokoban
{
    public class SokobanCli
    {
        public void Start()
        {
            const string path = "Resources/Levels/Level0.txt";
            var level = LevelParser.FromFile(path);
            var game = new Game(level);
            Print(level, game.GetCurrentState());

            while (true)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Escape:
                        Console.WriteLine();
                        return;

                    case ConsoleKey.R:
                        game.Reset();
                        break;

                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        game.MovePlayer(Direction.North);
                        break;
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.D:
                        game.MovePlayer(Direction.East);
                        break;
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        game.MovePlayer(Direction.South);
                        break;
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.A:
                        game.MovePlayer(Direction.West);
                        break;

                    case ConsoleKey.PageDown:
                        game.Undo();
                        break;
                    case ConsoleKey.PageUp:
                        game.Redo();
                        break;
                }

                Print(level, game.GetCurrentState());

                if (game.HasWon())
                {
                    Console.WriteLine("\nCongratulations you've solved the puzzle!\n");
                    return;
                }
            }
        }

        private static void Print(Level level, GameState gameState)
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
                if (gameState.PlayerPosition == point)
                {
                    return isTargetTile
                        ? LevelParser.PlayerOnTarget
                        : LevelParser.Player;
                }

                if (gameState.BoxPositions.Any(b => b == point))
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
