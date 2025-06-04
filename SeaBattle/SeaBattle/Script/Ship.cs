using SeaBattle.Script;
using System.Collections.Generic;
using System.Linq;

namespace SeaBattle.Script
{
    public class Ship : IShip
    {
        public Ship(int size)
        {
            Size = size;
        }

        public int Size { get; }

        public Direction Direction { get; set; } = Direction.Horizontal;

        public Point? Position { get; set; } = null;

        public IReadOnlyList<Point> GetPositionPoints()
        {
            if (Position.HasValue)
            {
                var p = Position.Value;
                return Enumerable
                    .Range(0, Size)
                    .Select(delta => Direction == Direction.Horizontal
                        ? new Point(p.X + delta, p.Y)
                        : new Point(p.X, p.Y + delta))
                    .ToList();
            }
            return new Point[0];
        }
    }
}
