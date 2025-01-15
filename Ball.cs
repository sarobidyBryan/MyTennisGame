using System.Drawing;

namespace TennisGame
{
    public class Ball : CircleShape
    {
        public Trajectory Trajectory { get;  set; }

        public Ball(int x, int y, int diameter, Color color,Trajectory trajectory)
            : base(x, y, diameter, color)
        {
            Trajectory = trajectory;
        }

        public void SetTrajectory(Trajectory trajectory)
        {
            Trajectory = trajectory;
        }

        public void UpdatePosition(Rectangle bounds)
        {
            if(Trajectory is HitLine)
            {
                HitLine ht = (HitLine) Trajectory;  
                ht.UpdatePosition(this,bounds);
            }
            else
            {
                Trajectory.UpdatePosition(this, bounds);
            }
            
        }

        public Point GetPosition()
        {
            return new Point(X, Y);
        }
    }
}
