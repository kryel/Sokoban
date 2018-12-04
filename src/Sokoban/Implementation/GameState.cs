using System.Collections.Immutable;

namespace Sokoban.Implementation
{
    internal class GameState
    {
        public Point PlayerPosition { get; }
        public IImmutableSet<Point> BoxPositions { get; }

        public GameState(Point playerPosition, IImmutableSet<Point> boxPositions)
        {
            PlayerPosition = playerPosition;
            BoxPositions = boxPositions;
        }
    }
}
