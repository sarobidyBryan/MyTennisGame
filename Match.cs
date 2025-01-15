using System;
using System.Collections.Generic;

namespace TennisGame
{
    public class Match
    {
        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }
        public List<Set> Sets { get; private set; }
        public Player Winner { get; private set; }
        private Set CurrentSet => Sets[^1];

        public Match(Player player1, Player player2)
        {
            Player1 = player1;
            Player2 = player2;
            Sets = new List<Set> { new Set(player1, player2) };
        }

        public Set GetCurrentSet()
        {
            return Sets[^1];
        }



        public void AddSet(Set set)
        {
            Sets.Add(set);
        }

        public bool IsMatchOver()
        {
            int player1SetsWon = Sets.Count(set => set.Winner == Player1);
            int player2SetsWon = Sets.Count(set => set.Winner == Player2);

            if (player1SetsWon == 3)
            {
                Winner = Player1;
                return true;
            }

            if (player2SetsWon == 3)
            {
                Winner = Player2;
                return true;
            }

            return false;
        }

        public void StartNewSet()
        {
            var newSet = new Set(Player1, Player2);
            Sets.Add(newSet);
            Console.WriteLine("Starting a new set...");
        }

    }
}