using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisGame
{
    public abstract class Shape : IMovable
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Color Color { get; set; }

        public int DirectionX;
        public int DirectionY;
         public int Speed = 10;

        protected Shape(int x, int y, int width, int height, Color color)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Color = color;
        }

        public abstract void Draw(Graphics graphics);

        public void Move(Rectangle bounds)
        {
            X = Math.Max(bounds.X, Math.Min(bounds.Right - Width, X + DirectionX * Speed));

            Y = Math.Max(bounds.Y, Math.Min(bounds.Bottom - Height, Y + DirectionY * Speed));
        }


        public void UpdateDirection(int dx, int dy)
        {
            DirectionX = dx;
            DirectionY = dy;
        }
    }
}
