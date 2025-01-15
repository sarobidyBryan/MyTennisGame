using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisGame
{
    public abstract class Trajectory
    {
        public abstract void UpdatePosition(Shape shape, Rectangle bounds);
    }
}
