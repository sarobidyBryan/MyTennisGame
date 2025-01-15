using System.Collections.Generic;
using System.Drawing;

namespace TennisGame
{
    public class CibleMovementService
    {
        private List<Shape> shapes;

        public CibleMovementService(List<Shape> shapes)
        {
            this.shapes = shapes;
        }

        public void UpdateMovement(Rectangle bounds)
        {
            foreach (var shape in shapes)
            {
                shape.Move(bounds); 
            }
        }
    }
}
