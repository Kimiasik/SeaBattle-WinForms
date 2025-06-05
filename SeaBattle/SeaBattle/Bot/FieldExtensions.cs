using SeaBattle.Script;
using System;
using System.Linq;

namespace SeaBattle.Bot
{
    public static class FieldExtensions
    {
        private static Random random = new Random();

        // Генеруємо випадкову точку пострілу, яка ще не була використана
        public static Point GenerateRandomShot(this IField field)
        {
            var shots = field.GetShots();

            // Спроба випадкового вибору до 1000 разів
            for (int i = 0; i < 1000; i++)
            {
                var x = random.Next(field.Width);
                var y = random.Next(field.Height);
                var point = new Point(x, y);

                if (!shots.Contains(point))
                    return point;
            }

            // Якщо випадковий пошук не допоміг — перебираємо всі точки послідовно
            for (int x = 0; x < field.Width; x++)
            {
                for (int y = 0; y < field.Height; y++)
                {
                    var point = new Point(x, y);
                    if (!shots.Contains(point))
                        return point;
                }
            }

            // Якщо усі точки зайняті — повертаємо (0,0)
            return new Point(0, 0);
        }

        // Автоматично розставляємо кораблі, намагаючись до 1000 разів
        public static bool ArrangeShipsAutomatically(this IField field)
        {
            for (int i = 0; i < 1000; i++)
            {
                field.RemoveAllShips();

                if (field.TryArrangeShips(1000))
                    return true;
            }
            return false;
        }

        // Прибираємо усі кораблі з поля (переміщаємо в невидиму точку)
        private static void RemoveAllShips(this IField field)
        {
            foreach (var ship in field.GetShips().Where(s => s.Position != null))
            {
                field.PutShip(ship, new Point(-1, -1));
            }
        }

        // Спроба розставити кораблі, роблячи не більше steps кроків
        private static bool TryArrangeShips(this IField field, int steps)
        {
            for (int i = 0; i < steps; i++)
            {
                var ship = field.GetShipToPutOrNull();
                if (ship == null)
                    break;

                var pos = new Point(random.Next(field.Width), random.Next(field.Height));
                field.PutShip(ship, pos);

                if (random.Next(2) == 1)
                    field.ChangeShipDirection(ship);

                // Якщо є конфлікти — прибираємо корабель назад
                if (field.GetConflictingPoints().Any())
                {
                    field.PutShip(ship, new Point(-1, -1));
                }
            }

            // Повертаємо true, якщо всі кораблі розставлені і немає конфліктів
            return field.GetShipToPutOrNull() == null && !field.GetConflictingPoints().Any();
        }
    }
}
