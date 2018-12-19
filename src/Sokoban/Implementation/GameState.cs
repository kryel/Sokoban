using System.Collections.Immutable;

namespace Sokoban.Implementation
{
    internal class GameState
    {
        public Tile PlayerPosition { get; }
        public IImmutableSet<Tile> BoxPositions { get; }

        public GameState(Tile playerPosition, IImmutableSet<Tile> boxPositions)
        {
            PlayerPosition = playerPosition;
            BoxPositions = boxPositions;
        }
    }
}
