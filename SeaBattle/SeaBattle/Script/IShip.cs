using SeaBattle.Script;
using System.Collections.Generic;
using System.Drawing;

namespace SeaBattle.Script
{
    public interface IShip
    {
        Direction Direction { get; }
        Point? Position { get; }
        int Size { get; }

        IReadOnlyList<Point> GetPositionPoints();
    }
}