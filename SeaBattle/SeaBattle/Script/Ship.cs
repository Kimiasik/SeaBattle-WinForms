using SeaBattle.Script;
using System.Collections.Generic;
using System.Linq;

namespace SeaBattle.Script
{
    // Реалізація інтерфейсу IShip
    public class Ship : IShip
    {
        public Ship(int size)
        {
            Size = size;
        }

        // Розмір корабля (кількість клітинок)
        public int Size { get; }

        // Позиція корабля (null, якщо не розміщений)
        public Point? Position { get; set; } = null;

        // Напрямок корабля (горизонтальний за замовчуванням)
        public Direction Direction { get; set; } = Direction.Horizontal;

        // Повертає всі точки, які займає корабель на полі
        public IReadOnlyList<Point> GetPositionPoints()
        {
            if (!Position.HasValue)
                return new Point[0];

            var p = Position.Value;
            return Enumerable.Range(0, Size)
                .Select(offset => Direction == Direction.Horizontal
                    ? new Point(p.X + offset, p.Y)
                    : new Point(p.X, p.Y + offset))
                .ToList();
        }
    }
}
