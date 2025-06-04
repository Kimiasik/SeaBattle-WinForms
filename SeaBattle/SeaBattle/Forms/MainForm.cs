using SeaBattle.Forms;
using SeaBattle.Properties;
using SeaBattle.Script;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace SeaBattle.Forms
{
    public partial class MainForm : Form
    {
        private Game game;

            
        public MainForm()
        {
            InitializeComponent();

            this.Icon = new Icon("icon.ico");
            game = new Game();
            game.StageChanged += Game_OnStageChanged;

            ShowStartScreen();
        }

        private void Game_OnStageChanged(GameStage stage)
        {
            switch (stage)
            {
                case GameStage.ArrangingShips:
                    ShowArrangingShipsScreen();
                    break;
                case GameStage.Battle:
                    ShowBattleScreen();
                    break;
                case GameStage.Finished:
                    ShowFinishedScreen();
                    break;
                case GameStage.NotStarted:
                default:
                    ShowStartScreen();
                    break;
            }
        }

        private void ShowStartScreen()
        {
            HideScreens();
            startControl.Configure(game);
            startControl.Show();
        }

        private void ShowArrangingShipsScreen()
        {
            HideScreens();
            arrangingControl.Configure(game);
            arrangingControl.Show();
        }

        private void ShowBattleScreen()
        {
            HideScreens();
            battleControl.Configure(game);
            battleControl.Show();
        }

        private void ShowFinishedScreen()
        {
            HideScreens();
            finishedControl.Configure(game);
            finishedControl.Show();
        }

        private void HideScreens()
        {
            startControl.Hide();
            arrangingControl.Hide();
            battleControl.Hide();
            finishedControl.Hide();
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {

        }
    }
}
