using System.Drawing;
using System.Windows.Forms;

namespace TennisGame
{
    public class TennisGameManager
    {
        private readonly Player player1;
        private readonly Player player2;
        private Ball gameBall;
        private readonly Rectangle courtBounds;
        private Player currentPlayer;
        private TargetIndicator targetIndicator;
        private Match match;
        private DataGridView scoreTable;

        public TennisGameManager(Rectangle courtBounds, Player player1, Player player2, Ball gameBall, TargetIndicator ti, Match match, DataGridView scoreTable)
        {
            this.courtBounds = courtBounds;
            this.player1 = player1;
            this.player2 = player2;
            this.gameBall = gameBall;
            this.currentPlayer = player1;
            this.targetIndicator = ti;
            this.match = match;
            this.scoreTable = scoreTable;

            int targetX = targetIndicator.CurrentValue;
            int targetY = currentPlayer == player1 ? courtBounds.Y + courtBounds.Height : courtBounds.Y;
            HitLine newTrajectory = new HitLine(gameBall.GetPosition(), new Point(targetX, targetY), 15);
            gameBall.SetTrajectory(newTrajectory);

            InitializeScoreTable();
        }

        private void InitializeScoreTable()
        {
            scoreTable.ColumnCount = 3;
            scoreTable.Columns[0].Name = "Set";
            scoreTable.Columns[1].Name = player1.Name;
            scoreTable.Columns[2].Name = player2.Name;
            scoreTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            scoreTable.AllowUserToAddRows = false;
            scoreTable.AllowUserToDeleteRows = false;
            scoreTable.ReadOnly = true;
            scoreTable.RowHeadersVisible = false;
            scoreTable.Location = new Point(10, 10);
            scoreTable.Size = new Size(300, 150);
            scoreTable.BringToFront();
        }

        public void UpdateGameState(List<Shape> shapes)
        {
            gameBall.Move(courtBounds);
            HitLine traj = (HitLine)gameBall.Trajectory;

            if (IsBallInOpponentCourt(gameBall.GetPosition(), currentPlayer))
            {
                Player otherPlayer = currentPlayer == player1 ? player2 : player1;
                if (otherPlayer.IsIntersecting(gameBall))
                {
                    ReverseBallDirection();
                    ResetTargetIndicator();
                }
            }

            Rectangle temp = new Rectangle(gameBall.GetPosition(), new Size(gameBall.Width, gameBall.Height));
            if (gameBall.GetPosition() == traj.GetEndPoint() || temp.Contains(traj.endPoint))
            {
                ReverseBallDirection();
                ResetTargetIndicator();
            }

            foreach (var shape in shapes)
            {
                if (shape is Cible cible && cible.IsIntersecting(gameBall))
                {
                    if ((currentPlayer == player1 && IsInOpponentHalf(gameBall.GetPosition(), true)) ||
                        (currentPlayer == player2 && IsInOpponentHalf(gameBall.GetPosition(), false)))
                    {
                        UpdatePlayerScore(currentPlayer, cible.Points);
                        ReverseBallDirection();
                        ResetTargetIndicator();
                        DisplayScore();
                    }
                }
            }
        }

        private bool IsBallInOpponentCourt(Point ballPosition, Player player)
        {
            if (player == player1)
            {
                return ballPosition.Y > courtBounds.Height / 2;
            }
            else
            {
                return ballPosition.Y < courtBounds.Height / 2;
            }
        }

        private bool IsInOpponentHalf(Point ballPosition, bool isPlayer1)
        {
            return isPlayer1
                ? ballPosition.Y > (courtBounds.Y + courtBounds.Height) / 2
                : ballPosition.Y < (courtBounds.Y + courtBounds.Height) / 2;
        }

        private void ReverseBallDirection()
        {
            if (gameBall.Trajectory is HitLine hitLine)
            {
                currentPlayer = currentPlayer == player1 ? player2 : player1;
                int targetY = currentPlayer == player1
                    ? courtBounds.Y + courtBounds.Height - (int)(gameBall.Height / 2)
                    : courtBounds.Y;
                int targetX = targetIndicator.CurrentValue;
                hitLine.startPoint = gameBall.GetPosition();
                hitLine.endPoint = new Point(targetX, targetY);
                hitLine.currentPosition = 0;
            }
        }

        private void UpdatePlayerScore(Player player, int pointValue)
        {
            int currentScore = match.GetCurrentSet().CurrentGame.Points[player];
            int newScore = currentScore;

            if(pointValue > 2)
            {
                var currentGame = match.GetCurrentSet().CurrentGame;
                currentGame.AddPoint(player, 50);

                if (currentGame.IsGameOver())
                {
                    Console.WriteLine($"{player.Name} wins the game!");
                    match.GetCurrentSet().AddGame(currentGame);

                    if (match.GetCurrentSet().IsSetOver(player1, player2))
                    {
                        Console.WriteLine($"{player.Name} wins the set!");

                        if (!match.IsMatchOver())
                        {
                            match.StartNewSet();
                        }
                        else
                        {
                            Console.WriteLine($"Match over! Winner: {player.Name}");
                            ShowEndGameDialog(player.Name);
                            return;
                        }
                    }
                    return;
                }
                return;
            }

            switch (currentScore)
            {
                case 0:
                    newScore = pointValue == 1 ? 15 : 30;
                    break;
                case 15:
                    newScore = pointValue == 1 ? 30 : 40;
                    break;
                case 30:
                    newScore = 40;
                    break;
                case 40:
                    var currentGame = match.GetCurrentSet().CurrentGame;
                    currentGame.AddPoint(player, 50);

                    
                    if (currentGame.IsGameOver())
                    {
                        Console.WriteLine($"{player.Name} wins the game!");
                        match.GetCurrentSet().AddGame(currentGame);

                        if (match.GetCurrentSet().IsSetOver(player1, player2))
                        {
                            Console.WriteLine($"{player.Name} wins the set!");

                            if (!match.IsMatchOver())
                            {
                                match.StartNewSet();
                            }
                            else
                            {
                                Console.WriteLine($"Match over! Winner: {player.Name}");
                                ShowEndGameDialog(player.Name);
                                return;
                            }
                        }
                        return;
                    }
                    return;
                default:
                    break;
            }

            match.GetCurrentSet().CurrentGame.AddPoint(player, newScore);

            Connexion conn = new Connexion("localhost", "tennis", "postgres", " ");
            int id = 0;
            if (player == player1)
            {
                id = 0;
            }
            else if (player == player2)
            {
                id = 1;
            }
            conn.InsertScoreHistory(id, currentScore, newScore);

            Console.WriteLine($"Score updated: {player.Name} -> {newScore}");
        }

        private void ResetTargetIndicator()
        {
            targetIndicator.MinValue = courtBounds.X;
            targetIndicator.MaxValue = courtBounds.X + courtBounds.Width;
            targetIndicator.CurrentValue = (int)(courtBounds.X + courtBounds.Width / 2);
        }

        private void DisplayScore()
        {
            scoreTable.Rows.Clear();
            for (int i = 0; i < match.Sets.Count; i++)
            {
                var set = match.Sets[i];
                scoreTable.Rows.Add($"Set {i + 1}", set.Player1Games, set.Player2Games);
            }
            int player1Sets = match.Sets.Count(set => set.Winner == player1);
            int player2Sets = match.Sets.Count(set => set.Winner == player2);
            scoreTable.Rows.Add("Score sets:", player1Sets, player2Sets);
        }

        private void ShowEndGameDialog(string winnerName)
        {
            var result = MessageBox.Show(
                $"{winnerName} est le gagnant! Rejouer?",
                "Fin du Match",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information
            );

            if (result == DialogResult.Yes)
            {
                Application.Restart();
            }
            else if (result == DialogResult.No)
            {
                Application.Exit();
            }
        }
    }
}
