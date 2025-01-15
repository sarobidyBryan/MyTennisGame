using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisGame
{
    public class CircleTrajectory : Trajectory
    {
        private double angle;
        private double speed;
        private int centerX, centerY;
        private int radius;

        public CircleTrajectory(int centerX, int centerY, int radius, double speed = 5)
        {
            this.centerX = centerX;
            this.centerY = centerY;
            this.radius = radius;
            this.speed = speed;
            this.angle = 0;
        }

        public override void UpdatePosition(Shape shape, Rectangle bounds)
        {
            angle += speed * 0.1;
            shape.X = centerX + (int)(radius * Math.Cos(angle));
            shape.Y = centerY + (int)(radius * Math.Sin(angle));

            shape.X = Math.Max(bounds.Left, Math.Min(bounds.Right - shape.Width, shape.X));
            shape.Y = Math.Max(bounds.Top, Math.Min(bounds.Bottom - shape.Height, shape.Y));
        }
    }

}
