using SeaBattle.Bot;
using SeaBattle.Forms;
using SeaBattle.Script;
using System.Linq;
using System.Windows.Forms;

namespace SeaBattle.Forms
{
    public partial class BattleControl : UserControl
    {
        private Game game;

        public BattleControl()
        {
            InitializeComponent();
        }

        // Налаштування контролу з грою
        public void Configure(Game game)
        {
            if (this.game != null)
                return;

            this.game = game;

            // Налаштовуємо поля гравця і бота
            humanFieldControl.Configure(game.FirstPlayer.Field, false);
            aiFieldControl.Configure(game.SecondPlayer.Field, true);

            // Підписка на клік по полю AI для пострілу людини
            aiFieldControl.ClickOnPoint += AiFieldControl_ClickOnPoint;

            // Підписка на подію готовності робити постріл (для AI)
            game.ReadyToShoot += Game_ReadyToShoot;
        }

        // Обробка кліку людини по полю AI
        private void AiFieldControl_ClickOnPoint(Point point, MouseEventArgs args)
        {
            if (args.Button == MouseButtons.Left && game.CurrentPlayer.Equals(game.FirstPlayer))
            {
                game.ShootTo(point);
            }
        }

        // Подія, коли AI готовий зробити постріл
        private void Game_ReadyToShoot()
        {
            if (game.Stage != GameStage.Battle)
                return;

            if (game.CurrentPlayer.Equals(game.SecondPlayer))
            {
                var shot = game.FirstPlayer.Field.GenerateRandomShot();
                game.ShootTo(shot);
            }
        }
    }
}
