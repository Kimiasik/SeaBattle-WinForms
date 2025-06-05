using System;
using System.Collections.Generic;
using System.Drawing;

namespace SeaBattle.Script
{
    // Інтерфейс, що описує функціонал ігрового поля
    public interface IField
    {
        int Height { get; }
        int Width { get; }

        // Змінити напрямок корабля (горизонтальний/вертикальний)
        bool ChangeShipDirection(IShip ship);

        // Отримати точки, де кораблі конфліктують (накладаються)
        ISet<Point> GetConflictingPoints();

        // Повернути список усіх кораблів на полі
        IReadOnlyList<IShip> GetShips();

        // Повернути список кораблів, що займають конкретну точку
        IReadOnlyList<IShip> GetShipsAt(Point point);

        // Повернути корабель, який ще не поставлений, або null
        IShip GetShipToPutOrNull();

        // Повернути множину всіх зроблених пострілів
        ISet<Point> GetShots();

        // Перевірити, чи є живі кораблі на полі
        bool HasAliveShips();

        // Перевірити, чи корабель живий (є не влучені точки)
        bool IsAlive(IShip ship);

        // Спробувати розмістити корабель у заданій точці
        bool PutShip(IShip ship, Point point);

        // Подія, що сповіщає про оновлення поля
        event Action Updated;
    }
}
