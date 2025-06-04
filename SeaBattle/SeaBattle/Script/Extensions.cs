using System.Collections.Generic;
using System.Drawing;

namespace SeaBattle.Script
{
    public static class Extensions
    {
        public static IEnumerable<Point> GetRoundPoints(this Point point)
        {
            for (int dx = -1; dx <= 1; dx++)
                for (int dy = -1; dy <= 1; dy++)
                    yield return new Point(point.X + dx, point.Y + dy);
        }
    }
}
