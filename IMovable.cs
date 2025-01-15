using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisGame
{
    internal interface IMovable
    {
        void Move(Rectangle bounds);
        void UpdateDirection(int dx, int dy);
    }
}
