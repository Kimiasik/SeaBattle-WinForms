using System.Collections.Generic;
using System.Drawing;

namespace SeaBattle.Script
{
    public static class Extensions
    {
        // Отримуємо всі точки навколо (включно з самою точкою)
        public static IEnumerable<Point> GetRoundPoints(this Point origin)
        {
            for (int offsetX = -1; offsetX <= 1; offsetX++)
            {
                for (int offsetY = -1; offsetY <= 1; offsetY++)
                {
                    yield return new Point(origin.X + offsetX, origin.Y + offsetY);
                }
            }
        }
    }
}
