using System;
using System.Drawing;

namespace TennisGame
{
    public class CircleShape : Shape
    {
        public CircleShape(int x, int y, int diameter, Color color)
            : base(x, y, diameter, diameter, color) 
        {
        }

        public override void Draw(Graphics graphics)
        {
            using (var brush = new SolidBrush(Color))
            {
                graphics.FillEllipse(brush, X, Y, Width, Height);
            }
        }
    }
}
