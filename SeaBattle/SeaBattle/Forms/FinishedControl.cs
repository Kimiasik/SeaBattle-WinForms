using SeaBattle.Script;
using System.Windows.Forms;

namespace SeaBattle.Forms
{
    public partial class FinishedControl : UserControl
    {
        private Game game;

        public FinishedControl()
        {
            InitializeComponent();
        }

        public void Configure(Game game)
        {
            if (this.game != null)
                return;

            this.game = game;

            humanFieldControl.Configure(game.FirstPlayer.Field, false);
            aiFieldControl.Configure(game.SecondPlayer.Field, false);

            if (game.FirstPlayer.Field.HasAliveShips())
            {
                winnerLabel.Text = "Переміг гравець";
            }
            else
            {
                winnerLabel.Text = "Переміг бот";
            }
        }
    }
}
