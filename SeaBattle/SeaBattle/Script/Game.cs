using System;
using System.Drawing;
using System.Linq;

namespace SeaBattle.Script
{
    public class Game
    {
        private GameStage stage = GameStage.NotStarted;
        private Player firstPlayer;
        private Player secondPlayer;
        private bool isFirstPlayerCurrent;
        private readonly GameOptions options = new GameOptions();

        public Game(Action<GameOptions> configureOptions = null)
        {
            configureOptions?.Invoke(options);
        }

        public GameStage Stage => stage;
        public event Action<GameStage> StageChanged;

        public IPlayer FirstPlayer => firstPlayer;
        public IPlayer SecondPlayer => secondPlayer;
        public IPlayer CurrentPlayer => isFirstPlayerCurrent ? firstPlayer : secondPlayer;
        public event Action<IPlayer> CurrentPlayerChanged;

        public event Action ReadyToShoot;

        public void Start(string firstPlayerName, string secondPlayerName)
        {
            firstPlayer = InitializePlayer(firstPlayerName);
            secondPlayer = InitializePlayer(secondPlayerName);
            isFirstPlayerCurrent = true;
            CurrentPlayerChanged?.Invoke(CurrentPlayer);
            SetStage(GameStage.ArrangingShips);
        }

        public bool CanEndArrangingCurrentPlayerShips =>
            stage == GameStage.ArrangingShips && PlayerReadyForBattle(CurrentPlayer);

        public void EndArrangingCurrentPlayerShips()
        {
            if (!CanEndArrangingCurrentPlayerShips)
                return;

            if (!CanBeginBattle)
            {
                SwitchPlayer();
                return;
            }

            SwitchPlayer();
            SetStage(GameStage.Battle);
        }

        public bool CanBeginBattle =>
            stage == GameStage.ArrangingShips
            && PlayerReadyForBattle(firstPlayer) && PlayerReadyForBattle(secondPlayer);

        public void ShootTo(Point point)
        {
            if (stage != GameStage.Battle)
                throw new InvalidOperationException("Game is not in Battle stage.");

            var targetPlayer = GetOpponentPlayer();
            var result = targetPlayer.Field.ShootTo(point);

            switch (result)
            {
                case ShotResult.Hit:
                    if (CheckWinCondition())
                        SetStage(GameStage.Finished);
                    else
                        ReadyToShoot?.Invoke();
                    break;

                case ShotResult.Miss:
                    SwitchPlayer();
                    ReadyToShoot?.Invoke();
                    break;

                case ShotResult.Cancel:
                    // Повторний постріл в ту ж точку — нічого не робимо
                    break;

                default:
                    throw new InvalidOperationException("Unexpected shot result.");
            }
        }

        private Player InitializePlayer(string name)
        {
            var field = options.CreateField();
            foreach (var ship in options.CreateFleet())
                field.AddShip(ship);

            return new Player(name, field);
        }

        private void SetStage(GameStage newStage)
        {
            stage = newStage;
            StageChanged?.Invoke(newStage);
        }

        private Player GetOpponentPlayer() => isFirstPlayerCurrent ? secondPlayer : firstPlayer;

        private void SwitchPlayer()
        {
            isFirstPlayerCurrent = !isFirstPlayerCurrent;
            CurrentPlayerChanged?.Invoke(CurrentPlayer);
        }

        private static bool PlayerReadyForBattle(IPlayer player)
        {
            return player.Field.GetShipToPutOrNull() == null && !player.Field.GetConflictingPoints().Any();
        }

        private bool CheckWinCondition()
        {
            var opponent = GetOpponentPlayer();
            return opponent != null && !opponent.Field.HasAliveShips();
        }
    }
}
