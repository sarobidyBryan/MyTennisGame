using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisGame
{
    public class ScoreHistory
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int PointsBefore { get; set; }
        public int PointsAfter { get; set; }
        public DateTime ActionTime { get; set; }
    }


}
