using SeaBattle.Script;
using System.Windows.Forms;

namespace SeaBattle.Forms
{
    public partial class FinishedControl : UserControl
    {
        private Game game;

        // Конструктор
        public FinishedControl()
        {
            InitializeComponent();
        }

        // Ініціалізація контролу грою
        public void Configure(Game game)
        {
            // Якщо вже налаштовано, виходимо
            if (this.game != null) return;

            this.game = game;

            // Відображаємо поля без приховування
            humanFieldControl.Configure(game.FirstPlayer.Field, fogOfWar: false);
            aiFieldControl.Configure(game.SecondPlayer.Field, fogOfWar: false);

            // Визначаємо переможця та відображаємо текст
            var playerHasAliveShips = game.FirstPlayer.Field.HasAliveShips();
            winnerLabel.Text = playerHasAliveShips ? "Переміг гравець" : "Переміг бот";
        }
    }
}
