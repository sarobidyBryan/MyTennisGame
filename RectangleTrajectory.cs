using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisGame;

public class RectangleTrajectory : Trajectory
{
    private int x, y, width, height;
    private int speed;
    private int currentSide; 

    public RectangleTrajectory(int x, int y, int width, int height, int speed = 10)
    {
        this.x = x;
        this.y = y;
        this.width = width;
        this.height = height;
        this.speed = speed;
        this.currentSide = 0;
    }

    public override void UpdatePosition(Shape shape, Rectangle bounds)
    {
      
        switch (currentSide)
        {
            case 0: 
                shape.X += speed;
                if (shape.X >= x + width) 
                {
                    currentSide = 1; 
                }
                break;
            case 1: 
                shape.Y += speed;
                if (shape.Y >= y + height)
                {
                    currentSide = 2; 
                }
                break;
            case 2:
                shape.X -= speed;
                if (shape.X <= x)
                {
                    currentSide = 3; 
                }
                break;
            case 3:
                shape.Y -= speed;
                if (shape.Y <= y) 
                {
                    currentSide = 0; 
                }
                break;
        }

       
        shape.X = Math.Max(bounds.Left, Math.Min(bounds.Right - shape.Width, shape.X));
        shape.Y = Math.Max(bounds.Top, Math.Min(bounds.Bottom - shape.Height, shape.Y));
    }
}
