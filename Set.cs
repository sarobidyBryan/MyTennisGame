using System;

namespace TennisGame
{
    public class Set
    {
        public int Player1Games { get; private set; }
        public int Player2Games { get; private set; }
        public Player Winner { get; private set; }
        public List<Game> Games { get; private set; }
        public Game CurrentGame { get; private set; }

        public Set(Player player1, Player player2)
        {
            Games = new List<Game>();
            StartNewGame(player1, player2);
        }

        public void AddGame(Game game)
        {
            if (Winner != null) return;

            if (game.Winner == game.Player1)
                Player1Games++;
            else if (game.Winner == game.Player2)
                Player2Games++;

            CheckWinner();

            if (Winner == null)
            {
                StartNewGame(game.Player1, game.Player2);
            }
        }

        private void StartNewGame(Player player1, Player player2)
        {
            CurrentGame = new Game(player1, player2);
            Games.Add(CurrentGame);
        }

        private void CheckWinner()
        {
            if (Player1Games >= 6 && Player1Games - Player2Games >= 2)
                Winner = CurrentGame.Player1;
            else if (Player2Games >= 6 && Player2Games - Player1Games >= 2)
                Winner = CurrentGame.Player2;
        }

       

        public bool IsSetOver(Player player1, Player player2)
        {
            if (Player1Games >= 6 && Player1Games - Player2Games >= 2)
            {
                Winner = player1;
                return true;
            }

            if (Player2Games >= 6 && Player2Games - Player1Games >= 2)
            {
                Winner = player2;
                return true;
            }

            return false;
        }
    }
}
