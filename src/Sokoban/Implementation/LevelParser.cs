using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;

namespace Sokoban.Implementation
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

        internal static Level FromFile(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"The requested level (at path: {path}) does not exist.");
            }

            var lines = File.ReadAllLines(path);
            var playerPosition = default(Point?);
            var walls = new HashSet<Point>();
            var boxPositions = new HashSet<Point>();
            var targetPositions = new HashSet<Point>();

            var height = lines.Length;
            var width = lines.Max(s => s.Length);

            for (var y = 0; y < lines.Length; y++)
            {
                var line = lines[y];
                for (var x = 0; x < line.Length; x++)
                {
                    ParseCharacter(line[x], new Point(x, y));
                }
            }

            if (playerPosition == default(Point?))
            {
                throw new InvalidOperationException("Level does not contain a player");
            }

            if (boxPositions.Count != targetPositions.Count)
            {
                throw new InvalidOperationException("Level does not contain the same number of boxes and targets");
            }

            return new Level(width, height, walls.ToImmutableHashSet(), targetPositions.ToImmutableHashSet(), boxPositions.ToImmutableHashSet(), playerPosition.Value);

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
