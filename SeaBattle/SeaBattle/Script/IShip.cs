using SeaBattle.Script;
using System.Collections.Generic;
using System.Drawing;

namespace SeaBattle.Script
{
    // Інтерфейс, що описує корабель
    public interface IShip
    {
        // Напрямок корабля: горизонтальний чи вертикальний
        Direction Direction { get; }

        // Позиція корабля на полі (null, якщо не розміщений)
        Point? Position { get; }

        // Розмір корабля (кількість клітинок)
        int Size { get; }

        // Повертає список точок, які займає корабель
        IReadOnlyList<Point> GetPositionPoints();
    }
}
