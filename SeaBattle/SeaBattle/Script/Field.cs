using SeaBattle.Script;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SeaBattle.Script
{
    public class Field : IField
    {
        private readonly HashSet<Ship> ships = new HashSet<Ship>();
        private readonly HashSet<Point> shots = new HashSet<Point>();


        public int Width { get; }
        public int Height { get; }

        public event Action Updated;

        public Field(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public void AddShip(Ship ship)
        {
            ships.Add(ship);
            Updated?.Invoke();
        }

        public IReadOnlyList<IShip> GetShips()
        {
            return ships.ToList();
        }

        public IShip GetShipToPutOrNull()
        {
            // Беремо найбільший корабель, який ще не розміщений
            return ships
                .Where(s => !s.Position.HasValue)
                .OrderByDescending(s => s.Size)
                .FirstOrDefault();
        }

        public bool PutShip(IShip ship, Point point)
        {
            if (!ships.Contains(ship))
                throw new InvalidOperationException("Ship is not part of this field.");

            var actualShip = (Ship)ship;

            int dx = actualShip.Direction == Direction.Horizontal ? actualShip.Size : 1;
            int dy = actualShip.Direction == Direction.Vertical ? actualShip.Size : 1;

            bool isInsideBounds = point.X >= 0 && point.Y >= 0 &&
                                  point.X + dx <= Width && point.Y + dy <= Height;

            if (isInsideBounds)
            {
                actualShip.Position = point;
                Updated?.Invoke();
                return true;
            }
            else
            {
                actualShip.Position = null;
                Updated?.Invoke();
                return false;
            }
        }

        public IReadOnlyList<IShip> GetShipsAt(Point point)
        {
            return ships
                .Where(ship => ship.GetPositionPoints().Contains(point))
                .OrderBy(ship => ship.Size)
                .ToList();
        }

        public bool ChangeShipDirection(IShip ship)
        {
            if (!ships.Contains(ship))
                throw new InvalidOperationException("Ship is not part of this field.");

            var actualShip = (Ship)ship;

            if (!actualShip.Position.HasValue)
                return false;

            var pos = actualShip.Position.Value;

            if (actualShip.Direction == Direction.Horizontal)
            {
                int overflow = pos.Y + actualShip.Size - Height;
                if (overflow > 0)
                {
                    var adjustedPos = new Point(pos.X, pos.Y - overflow);
                    if (adjustedPos.Y < 0)
                    {
                        actualShip.Position = null;
                        Updated?.Invoke();
                        return false;
                    }
                    actualShip.Position = adjustedPos;
                }
                actualShip.Direction = Direction.Vertical;
            }
            else
            {
                int overflow = pos.X + actualShip.Size - Width;
                if (overflow > 0)
                {
                    var adjustedPos = new Point(pos.X - overflow, pos.Y);
                    if (adjustedPos.X < 0)
                    {
                        actualShip.Position = null;
                        Updated?.Invoke();
                        return false;
                    }
                    actualShip.Position = adjustedPos;
                }
                actualShip.Direction = Direction.Horizontal;
            }

            Updated?.Invoke();
            return true;
        }

        public ISet<Point> GetConflictingPoints()
        {
            // Створюємо словник корабель -> всі точки навколо нього
            var shipSurroundings = ships.ToDictionary(
                ship => ship,
                ship => GetShipRoundPoints(ship)
            );

            var conflicts = new HashSet<Point>();

            foreach (var ship in ships)
            {
                var occupiedPoints = ship.GetPositionPoints();

                foreach (var point in occupiedPoints)
                {
                    bool conflictExists = shipSurroundings
                        .Any(pair => !pair.Key.Equals(ship) && pair.Value.Contains(point));

                    if (conflictExists)
                        conflicts.Add(point);
                }
            }

            return conflicts;
        }

        public ShotResult ShootTo(Point point)
        {
            if (shots.Contains(point))
                return ShotResult.Cancel;

            shots.Add(point);

            var targetShip = GetShipsAt(point).FirstOrDefault();

            if (targetShip == null)
            {
                Updated?.Invoke();
                return ShotResult.Miss;
            }

            // Перевіряємо, чи всі точки корабля уражені, якщо так, корабель потоплений
            bool isDestroyed = targetShip.GetPositionPoints().All(p => shots.Contains(p));

            if (isDestroyed)
            {
                shots.UnionWith(GetShipRoundPoints(targetShip));
            }

            Updated?.Invoke();
            return ShotResult.Hit;
        }

        private HashSet<Point> GetShipRoundPoints(IShip ship)
        {
            return ship.GetPositionPoints()
                .SelectMany(p => p.GetRoundPoints())
                .Where(p => p.X >= 0 && p.X < Width && p.Y >= 0 && p.Y < Height)
                .ToHashSet();
        }

        public ISet<Point> GetShots()
        {
            return shots.ToHashSet();
        }

        public bool IsAlive(IShip ship)
        {
            if (!ships.Contains(ship))
                throw new InvalidOperationException("Ship is not part of this field.");

            // Якщо хоч одна точка корабля не влучена, то корабель живий
            return ship.GetPositionPoints().Any(p => !shots.Contains(p));
        }

        public bool HasAliveShips()
        {
            return ships.Any(IsAlive);
        }
    }
}
