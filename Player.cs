using System;

namespace TennisGame
{
    public class Player : RectangleShape
    {
        public string Name { get; set; }
        

        public Player(string name, int x, int y, int height, int width, Color color)
            : base(x, y, height, width, color)
        {
            Name = name;
        }

        public bool IsIntersecting(Ball ball)
        {
            Rectangle playerRect = new Rectangle(X, Y, Width, Height);
            Point ballCenter = ball.GetPosition();
            int ballRadius = ball.Width / 2;

            if (playerRect.Contains(ballCenter))
            {
                return true;
            }

            int closestX = Math.Clamp(ballCenter.X, playerRect.Left, playerRect.Right);
            int closestY = Math.Clamp(ballCenter.Y, playerRect.Top, playerRect.Bottom);

            int distanceX = ballCenter.X - closestX;
            int distanceY = ballCenter.Y - closestY;

            return (distanceX * distanceX + distanceY * distanceY) <= (ballRadius * ballRadius);
        }
    }
}
