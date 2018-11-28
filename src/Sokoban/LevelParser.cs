using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sokoban
{
    public class LevelParser
    {
        public const char EmptyTile = ' ';
        public const char WallTile = '#';
        public const char TargetTile = '/';
        public const char Player = '+';
        public const char PlayerOnTarget = '=';
        public const char Box = 'O';
        public const char BoxOnTarget = 'Ø';

        internal static Level FromFile(string levelName)
        {
            var lines = File.ReadAllLines($"Resources/Levels/{levelName}");

            var playerPosition = default(Point?);
            var walls = new HashSet<Point>();
            var boxPositions = new HashSet<Point>();
            var targetPositions = new HashSet<Point>();

            var height = lines.Length;
            var width = lines.Max(s => s.Length);

            var y = 0;
            foreach (var line in lines)
            {
                var x = 0;
                foreach (var character in line)
                {
                    ParseCharacter(character, new Point(x, y));
                    x++;
                }

                y++;
            }

            if (playerPosition == default(Point?))
            {
                throw new InvalidOperationException("Level does not contain a player");
            }

            if (boxPositions.Count != targetPositions.Count)
            {
                throw new InvalidOperationException("Level does not contain the same number of boxes and targets");
            }

            return new Level(width, height, walls, boxPositions, targetPositions, playerPosition.Value);

            void ParseCharacter(char c, Point point)
            {
                switch (c)
                {
                    case EmptyTile:
                        break;
                    case WallTile:
                        walls.Add(point);
                        break;
                    case TargetTile:
                        targetPositions.Add(point);
                        break;
                    case Player:
                        playerPosition = point;
                        break;
                    case PlayerOnTarget:
                        playerPosition = Point.Clone(point);
                        targetPositions.Add(point);
                        break;
                    case Box:
                        boxPositions.Add(point);
                        break;
                    case BoxOnTarget:
                        targetPositions.Add(point);
                        boxPositions.Add(point);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
