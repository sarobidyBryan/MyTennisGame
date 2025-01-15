using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisGame
{
    public class GameBall: Ball
    {
        public GameBall(int x, int y, int diameter, System.Drawing.Color color, Trajectory trajectory) : base(x, y, diameter, color, trajectory)
        {
        }
    }
}
