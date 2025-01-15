using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TennisGame
{
    public class RectangleShape: Shape
    {
        public RectangleShape(int x, int y, int width, int height, Color color)
        : base(x, y, width, height, color)
        {
        }

        public override void Draw(Graphics graphics)
        {
            using (var brush = new SolidBrush(Color))
            {
                graphics.FillRectangle(brush, X, Y, Width, Height);
            }
        }

    }
}
