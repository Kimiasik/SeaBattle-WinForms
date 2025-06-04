using System;
using System.Collections.Generic;
using System.Drawing;

namespace SeaBattle.Script
{
    public interface IField
    {
        int Height { get; }
        int Width { get; }

        bool ChangeShipDirection(IShip ship);
        ISet<Point> GetConflictingPoints();
        IReadOnlyList<IShip> GetShips();
        IReadOnlyList<IShip> GetShipsAt(Point point);
        IShip GetShipToPutOrNull();
        ISet<Point> GetShots();
        bool HasAliveShips();
        bool IsAlive(IShip ship);
        bool PutShip(IShip ship, Point point);

        event Action Updated;
    }
}