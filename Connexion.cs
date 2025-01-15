using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisGame
{
    public class Connexion
    {
        private readonly string _connectionString;

        public Connexion(string host, string database, string username, string password)
        {
            _connectionString = $"Host={host};Database={database};Username={username};Password='{password}'";
        }

        public void Connect()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Connection to database established successfully.");
                    // Execute queries or commands here
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }



        public void InsertData(string tableName, string columnName, string value)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    string query = $"INSERT INTO {tableName} ({columnName}) VALUES (@value)";

                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@value", value);
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine($"{rowsAffected} row(s) inserted.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }


        public void InsertScoreHistory(int playerId, int pointsBefore, int pointsAfter)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                INSERT INTO score_history (player_id, points_before, points_after)
                VALUES (@player_id, @points_before, @points_after)";

                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@player_id", playerId);
                        command.Parameters.AddWithValue("@points_before", pointsBefore);
                        command.Parameters.AddWithValue("@points_after", pointsAfter);

                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine($"{rowsAffected} row(s) inserted into score_history.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while inserting into score_history: {ex.Message}");
                }
            }
        }


        public List<ScoreHistory> GetScoreHistories()
        {
            var scoreHistories = new List<ScoreHistory>();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT id, player_id, points_before, points_after, action_time FROM score_history";

                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var scoreHistory = new ScoreHistory
                                {
                                    Id = reader.GetInt32(0),
                                    PlayerId = reader.GetInt32(1),
                                    PointsBefore = reader.GetInt32(2),
                                    PointsAfter = reader.GetInt32(3),
                                    ActionTime = reader.GetDateTime(4)
                                };
                                scoreHistories.Add(scoreHistory);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }

            return scoreHistories;
        }
    }
}
