using System.Drawing;

public class Terrain 
{
     public int X { get; set; }
     public int Y {get; set;}
     public int Width {get; set;}
     public int Height {get; set;}
     public Rectangle Rect { get; set;}
    public Terrain(int x, int y, int width, int height)
    {
        this.X = x;
        this.Y = y;
        this.Width = width;
        this.Height = height;
        Rect = new Rectangle(x, y, width, height);
    }

    public void Draw(Graphics graphics)
    {
        using (var pen = new Pen(Color.Red, 3))
        {
            graphics.DrawRectangle(pen, this.Rect);
        }
    }
}
