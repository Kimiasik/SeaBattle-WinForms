using SeaBattle.Script;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace SeaBattle.Forms
{
    public partial class FieldControl : UserControl
    {
        private IField field;
        private bool fogOfWar;
        private Dictionary<Script.Point, Rectangle> pointToRectangle;
        private IShip selectedShip = null;
        private bool configured = false;

        public FieldControl()
        {
            InitializeComponent();
            DoubleBuffered = true;

            // Підписка на події зміни розміру і кліку
            Resize += HandleResize;
            Click += HandleClick;
            DoubleClick += HandleClick;
        }

        // Налаштовує поле для відображення.
        public void Configure(IField field, bool fogOfWar)
        {
            if (configured)
                throw new InvalidOperationException("FieldControl уже налаштовано.");

            this.field = field;
            this.fogOfWar = fogOfWar;

            // Перестворюємо карту клітинок
            pointToRectangle = GeneratePointToRectangle(field, Width, Height);

            // Підписка на оновлення поля для оновлення контролу
            this.field.Updated += () => Invalidate();

            configured = true;
        }

        // Подія кліку по конкретній клітинці поля.
        public event Action<Script.Point, MouseEventArgs> ClickOnPoint;

        // Встановлення вибраного корабля (виділення).
        public void SetSelectedShip(IShip selectedShip)
        {
            this.selectedShip = selectedShip;
            Invalidate();
        }

        public IShip SelectedShip => selectedShip;

        // Малюємо поле, кораблі, постріли та виділення.
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Малюємо фон (розширення Graphics методом розширення)
            e.Graphics.DrawBackground(Width, Height);

            if (!configured)
                return;

            // Малюємо порожні клітинки поля
            foreach (var pair in pointToRectangle)
                e.Graphics.DrawEmptyCell(pair.Value);

            var conflictingPoints = field.GetConflictingPoints();
            var shots = field.GetShots();

            // Малюємо клітинки з пострілами
            foreach (var shot in shots)
                e.Graphics.DrawShotCell(pointToRectangle[shot]);

            // Малюємо всі кораблі, окрім вибраного
            foreach (var ship in field.GetShips().Where(it => !it.Equals(selectedShip)))
                PaintShip(e.Graphics, ship, conflictingPoints, shots, useLight: false);

            // Малюємо вибраний корабель зверху з підсвіткою
            if (selectedShip != null)
                PaintShip(e.Graphics, selectedShip, conflictingPoints, shots, useLight: true);
        }

        // Малює один корабель.
        private void PaintShip(Graphics graphics, IShip ship,
            ISet<Script.Point> conflictingPoints, ISet<Script.Point> shots, bool useLight)
        {
            foreach (var point in ship.GetPositionPoints())
            {
                // Якщо туман війни відсутній або клітинка вже відкрита (пострілом)
                if (!fogOfWar || shots.Contains(point))
                {
                    graphics.DrawShipCell(pointToRectangle[point],
                        useLight: useLight,
                        inConflict: conflictingPoints.Contains(point),
                        isShot: shots.Contains(point));
                }
            }
        }

 
        // Обробник зміни розміру контролу — перевираховує розміри клітинок.
        private void HandleResize(object sender, EventArgs e)
        {
            if (!configured)
                return;

            pointToRectangle = GeneratePointToRectangle(field, Width, Height);
            Invalidate();
        }

        // Обробник кліку миші — визначає, по якій клітинці клікнули.

        private void HandleClick(object sender, EventArgs e)
        {
            if (!configured)
                return;

            var args = e as MouseEventArgs;
            var hitCells = pointToRectangle.Where(it => it.Value.Contains(args.Location)).ToList();

            if (hitCells.Count > 0)
                ClickOnPoint?.Invoke(hitCells[0].Key, args);
        }

        // Генерує словник відповідності клітинок поля до прямокутників для відображення.
   
        private static Dictionary<Script.Point, Rectangle> GeneratePointToRectangle(IField field, int width, int height)
        {
            var result = new Dictionary<Script.Point, Rectangle>();

            for (var x = 0; x < field.Width; x++)
            {
                for (int y = 0; y < field.Height; y++)
                {
                    // Обчислюємо розміри клітинки з невеликими відступами
                    var left = (width - 2) * x / field.Width + 1;
                    var right = (width - 2) * (x + 1) / field.Width + 1;
                    var top = (height - 2) * y / field.Height + 1;
                    var bottom = (height - 2) * (y + 1) / field.Height + 1;

                    var rectangle = new Rectangle(left, top, right - left, bottom - top);
                    result.Add(new Script.Point(x, y), rectangle);
                }
            }

            return result;
        }
    }
}
