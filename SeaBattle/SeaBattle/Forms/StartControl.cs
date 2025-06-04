using SeaBattle.Script;
using System;
using System.Windows.Forms;

namespace SeaBattle.Forms
{
    public partial class StartControl : UserControl
    {
        private Game game;

        public StartControl()
        {
            InitializeComponent();
        }

        public void Configure(Game game)
        {
            if (this.game != null)
                return;

            this.game = game;

            startButton.Click += StartButton_Click;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            game.Start("Player", "Bot");
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {

        }
    }
}
