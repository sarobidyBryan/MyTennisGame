using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisGame
{
    public class Cible : Ball
    {
        public int Points { get; set; }

        public Cible(int x, int y, int diameter, Color color, Trajectory trajectory, int points)
            : base(x, y, diameter, color, trajectory)
        {
            Points = points;
        }

        public bool IsIntersecting(Ball ball)
        {
            Rectangle cibleRect = new Rectangle(X, Y, Width, Height);
            Point ballCenter = ball.GetPosition();
            int ballRadius = ball.Width / 2;

            if (cibleRect.Contains(ballCenter))
            {
                return true;
            }

            int closestX = Math.Clamp(ballCenter.X, cibleRect.Left, cibleRect.Right);
            int closestY = Math.Clamp(ballCenter.Y, cibleRect.Top, cibleRect.Bottom);

            int distanceX = ballCenter.X - closestX;
            int distanceY = ballCenter.Y - closestY;

            return (distanceX * distanceX + distanceY * distanceY) <= (ballRadius * ballRadius);
        }
    }
}
