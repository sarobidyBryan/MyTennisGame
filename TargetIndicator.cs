using System;
using System.Drawing;
using System.Windows.Forms;

namespace TennisGame
{
    public class TargetIndicator
    {
        public int MinValue { get;  set; }
        public int MaxValue { get; set; } 
        public int CurrentValue { get; set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        private int xPosition; 
        private int yPosition; 

        public TargetIndicator(int xPosition, int yPosition, int minValue, int maxValue, int width, int height)
        {
            this.xPosition = xPosition;
            this.yPosition = yPosition;
            this.MinValue = minValue;
            this.MaxValue = maxValue;
            this.Width = width;
            this.Height = height;
            this.CurrentValue = (minValue + maxValue) / 2;
        }

        public void Draw(Graphics g)
        {
            using (Brush barBrush = new SolidBrush(Color.Red))
            {
                g.FillRectangle(barBrush, xPosition, yPosition, Width, Height);
            }

            int pointerX = (int)((float)(CurrentValue - MinValue) / (MaxValue - MinValue) * Width) + xPosition;
            using (Brush pointerBrush = new SolidBrush(Color.Black))
            {
                g.FillRectangle(pointerBrush, pointerX - 5, yPosition - 10, 10, Height + 20); 
            }
        }

        public void MoveLeft()
        {
            if (CurrentValue > MinValue)
            {
                CurrentValue-=10;
            }
        }

        public void MoveRight()
        {
            if (CurrentValue < MaxValue)
            {
                CurrentValue+=10;
            }
        }

        public int GetTargetPosition()
        {
            return CurrentValue;
        }
    }
}
