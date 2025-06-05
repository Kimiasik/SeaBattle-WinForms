using SeaBattle.Bot;
using SeaBattle.Script;
using System.Linq;
using System.Windows.Forms;

namespace SeaBattle.Forms
{
    public partial class ArrangingControl : UserControl
    {
        private Game game;

        public ArrangingControl()
        {
            InitializeComponent();
        }

        // Ініціалізація контролу з грою
        public void Configure(Game game)
        {
            if (this.game != null)
                return;

            this.game = game;

            endArrangingButton.Enabled = game.CanEndArrangingCurrentPlayerShips;
            endArrangingButton.Click += EndArrangingButton_Click;

            // Підписка на оновлення поля для оновлення кнопки
            game.FirstPlayer.Field.Updated += () =>
            {
                endArrangingButton.Enabled = game.CanEndArrangingCurrentPlayerShips;
            };

            fieldControl.Configure(game.FirstPlayer.Field, false);
            fieldControl.ClickOnPoint += FieldControl_ClickOnPoint;
        }

        // Обробка кліку по полю
        private void FieldControl_ClickOnPoint(Point point, MouseEventArgs args)
        {
            if (args.Button == MouseButtons.Right)
            {
                fieldControl.SetSelectedShip(null);
                return;
            }

            if (args.Button != MouseButtons.Left)
                return;

            var selectedShip = fieldControl.SelectedShip;

            if (selectedShip != null)
            {
                if (selectedShip.GetPositionPoints().Contains(point))
                    game.CurrentPlayer.Field.ChangeShipDirection(selectedShip);
                else
                    game.CurrentPlayer.Field.PutShip(selectedShip, point);

                fieldControl.SetSelectedShip(null);
                return;
            }

            var shipAtPoint = game.CurrentPlayer.Field.GetShipsAt(point).FirstOrDefault();

            if (shipAtPoint != null)
            {
                fieldControl.SetSelectedShip(shipAtPoint);
                return;
            }

            var shipToPlace = game.CurrentPlayer.Field.GetShipToPutOrNull();

            if (shipToPlace != null)
                game.CurrentPlayer.Field.PutShip(shipToPlace, point);
        }

        // Обробка натискання кнопки завершення розміщення кораблів
        private void EndArrangingButton_Click(object sender, System.EventArgs e)
        {
            if (game.CurrentPlayer.Equals(game.FirstPlayer))
            {
                game.EndArrangingCurrentPlayerShips();
                return;
            }

            // Для бота намагаємось розставити кораблі автоматично
            if (game.CurrentPlayer.Field.ArrangeShipsAutomatically())
                game.EndArrangingCurrentPlayerShips();
            else
                MessageBox.Show("Не Вдалося розмістити кораблі бота", "Повідомлення", MessageBoxButtons.OK);
        }

        private void headerLabel_Click(object sender, System.EventArgs e)
        {
            // Порожній хендлер, можна видалити, якщо не використовується
        }
    }
}
