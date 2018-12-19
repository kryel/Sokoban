using System;

namespace Sokoban.Implementation
{
    internal struct Tile : IEquatable<Tile>
    {
        public readonly int X;
        public readonly int Y;

        public Tile(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Tile Clone(Tile tile)
        {
            return new Tile(tile.X, tile.Y);
        }

        public bool Equals(Tile other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is Tile point && Equals(point);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        public static Tile operator +(Tile tile, Direction direction)
        {
            return new Tile(tile.X + direction.X, tile.Y + direction.Y);
        }

        public static bool operator ==(Tile a, Tile b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Tile a, Tile b)
        {
            return !(a == b);
        }
    }
}
