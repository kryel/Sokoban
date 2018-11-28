namespace Sokoban
{
    internal struct Direction
    {
        public static readonly Direction North = new Direction(0, -1);
        public static readonly Direction East = new Direction(1, 0);
        public static readonly Direction South = new Direction(0, 1);
        public static readonly Direction West = new Direction(-1, 0);

        public readonly int X;
        public readonly int Y;

        private Direction(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
