namespace TennisGame
{
    public class Game
    {
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public Dictionary<Player, int> Points { get; private set; }
        public Player Winner { get; private set; }

        public Game(Player player1, Player player2)
        {
            Player1 = player1;
            Player2 = player2;

            Points = new Dictionary<Player, int>
        {
            { Player1, 0 },
            { Player2, 0 }
        };
        }

        public void AddPoint(Player player, int newScore)
        {
            if (!Points.ContainsKey(player))
            {
                throw new ArgumentException("Player does not belong to this game.");
            }

            Points[player] = newScore;

            Console.WriteLine($"{player.Name} scored! Current points: {Points[player]}");

            if (IsGameOver())
            {
                Winner = player;
                Console.WriteLine($"{Winner.Name} wins the game!");
            }
        }

        public bool IsGameOver()
        {
            foreach (var point in Points)
            {
                if (point.Value >= 40)
                {
                    Winner = point.Key;
                    return true;
                }
            }
            return false;
        }
    }
}
