using System.Collections.Immutable;

namespace Sokoban.Implementation
{
    internal class Level
    {
        public int Width { get; }
        public int Height { get; }
        public IImmutableSet<Point> Walls { get; }
        public IImmutableSet<Point> TargetPositions { get; }
        public IImmutableSet<Point> BoxStartingPositions { get; }
        public Point PlayerStartingPosition { get; }

        internal Level(int width, int height, IImmutableSet<Point> walls, IImmutableSet<Point> targetPositions, IImmutableSet<Point> boxStartingPositions, Point playerStartingPosition)
        {
            Width = width;
            Height = height;
            Walls = walls;
            TargetPositions = targetPositions;
            BoxStartingPositions = boxStartingPositions;
            PlayerStartingPosition = playerStartingPosition;
        }
    }
}
