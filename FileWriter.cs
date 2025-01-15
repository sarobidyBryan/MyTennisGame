using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisGame
{

    public class FileWriter
    {
        public void WriteScoreHistoriesToFile(List<ScoreHistory> scoreHistories, string filePath)
        {
            try
            {
            
                using (var writer = new StreamWriter(filePath, false))
                {
                    writer.WriteLine("Id,PlayerId,PointsBefore,PointsAfter,ActionTime");

                    foreach (var scoreHistory in scoreHistories)
                    {
                        writer.WriteLine($"{scoreHistory.Id},{scoreHistory.PlayerId},{scoreHistory.PointsBefore},{scoreHistory.PointsAfter},{scoreHistory.ActionTime}");
                    }
                }

                Console.WriteLine($"Data successfully written to {filePath}");


                OpenFile(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void OpenFile(string filePath)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true
                });
                Console.WriteLine($"File {filePath} opened successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while opening the file: {ex.Message}");
            }
        }
    }
}
