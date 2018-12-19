using System.Collections.Immutable;

namespace Sokoban.Implementation
{
    internal class Level
    {
        public int Width { get; }
        public int Height { get; }
        public IImmutableSet<Tile> Walls { get; }
        public IImmutableSet<Tile> TargetPositions { get; }
        public IImmutableSet<Tile> BoxStartingPositions { get; }
        public Tile PlayerStartingPosition { get; }

        internal Level(int width, int height, IImmutableSet<Tile> walls, IImmutableSet<Tile> targetPositions, IImmutableSet<Tile> boxStartingPositions, Tile playerStartingPosition)
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
