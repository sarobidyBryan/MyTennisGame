using System.Drawing;
using System.Windows.Forms;

namespace TennisGame
{

    public class TerrainControl : Control
    {
        private Terrain terrain;
        private List<Shape> shapes;
        
        public TerrainControl()
        {
            this.DoubleBuffered = true;
            shapes = new List<Shape>();
        }

        public void SetTerrain(Terrain terrain)
        {
            this.terrain = terrain;
            this.Invalidate();
        }

        public void SetShapes(List<Shape> shapes)
        {
            this.shapes = shapes;
            this.Invalidate(); 
        }



        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (terrain != null)
            {
                Image terrainImage = Image.FromFile("D:/ITU/prog/TennisGame/court.png");

                e.Graphics.DrawImage(terrainImage, terrain.X, terrain.Y, terrain.Width, terrain.Height);
            }

            foreach (var shape in shapes)
            {
                shape.Draw(e.Graphics);
            }
        }
    }
}