using System.Collections.Generic;
using System.Linq;

namespace Sokoban
{
    internal class Level
    {
        public int Width { get; }
        public int Height { get; }
        public ICollection<Point> Walls { get; }
        public ICollection<Point> BoxPositions { get; }
        public ICollection<Point> TargetPositions { get; }
        public Point PlayerPosition { get; private set; }

        public Level(int width, int height, ICollection<Point> walls, ICollection<Point> boxPositions, ICollection<Point> targetPositions, Point playerPosition)
        {
            Width = width;
            Height = height;
            Walls = walls;
            BoxPositions = boxPositions;
            TargetPositions = targetPositions;
            PlayerPosition = playerPosition;
        }

        public void MovePlayer(Direction direction)
        {
            var nextPosition = PlayerPosition + direction;
            if (Walls.Contains(nextPosition))
            {
                return;
            }

            if (BoxPositions.Contains(nextPosition))
            {
                var behindNextPosition = nextPosition + direction;
                if (Walls.Contains(behindNextPosition) || BoxPositions.Contains(behindNextPosition))
                {
                    return;
                }

                BoxPositions.Add(behindNextPosition);
                BoxPositions.Remove(nextPosition);
            }

            PlayerPosition = nextPosition;
        }

        public bool HasWon()
        {
            return BoxPositions.All(p => TargetPositions.Contains(p));
        }
    }
}
