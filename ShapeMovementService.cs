using System.Collections.Generic;
using System.Drawing;

namespace TennisGame
{
    public class ShapeMovementService
    {
        private List<Shape> shapes;

        public ShapeMovementService(List<Shape> shapes)
        {
            this.shapes = shapes;
        }

        public void UpdateMovement(Rectangle bounds)
        {
            foreach (var shape in shapes)
            {
                if (shape is Ball ball)
                {
                    ball.UpdatePosition(bounds); 
                }
                else
                {
                    shape.Move(bounds);
                }
            }
        }
    }
}
