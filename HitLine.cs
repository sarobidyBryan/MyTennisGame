using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TennisGame
{
    public class HitLine: LineTrajectory
    {
        public HitLine(Point startPoint, Point endPoint, double speed = 10)
        : base(startPoint, endPoint, speed)
        {

        }

        public override void UpdatePosition(Shape shape, Rectangle bounds)
        {
            double dx = endPoint.X - startPoint.X;
            double dy = endPoint.Y - startPoint.Y;
            double distance = Math.Sqrt(dx * dx + dy * dy);

            currentPosition += speed / distance;

            if (currentPosition >= 1)
            {
                currentPosition = 1;
            }

            shape.X = (int)(startPoint.X + currentPosition * dx);
            shape.Y = (int)(startPoint.Y + currentPosition * dy);

            shape.X = Math.Max(bounds.Left, Math.Min(bounds.Right - shape.Width, shape.X));
            shape.Y = Math.Max(bounds.Top, Math.Min(bounds.Bottom - shape.Height, shape.Y));
        }
    }
}
