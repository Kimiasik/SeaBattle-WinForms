using SeaBattle.Script;
using System.Collections.Generic;

namespace SeaBattle.Script
{
    public class GameOptions
    {
        // Масив розмірів кораблів у флоті за замовчуванням
        private int[] sizes = new[] { 1, 1, 1, 1, 2, 2, 2, 3, 3, 4 };

        // Розміри ігрового поля
        public int Width { get; set; } = 10;
        public int Height { get; set; } = 10;

        // Створює флот кораблів згідно з поточними розмірами
        public IEnumerable<Ship> CreateFleet()
        {
            foreach (var size in sizes)
                yield return new Ship(size);
        }

        // Оновлює список розмірів кораблів
        public void SetShipSizes(params int[] sizes)
        {
            this.sizes = sizes;
        }

        // Створює нове ігрове поле з заданими розмірами
        public Field CreateField()
        {
            return new Field(Width, Height);
        }
    }
}
