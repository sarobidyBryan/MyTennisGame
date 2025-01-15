namespace TennisGame
{
    partial class Form1
    {
        
        private TerrainControl terrainControl;

       
       public int widthFenetre = 1024, heightFenetre = 700;
       public int widthTerrain = (int)(1024 / 3), heightTerrain = 700 - 100,
            xTerrain = (int)((1024 - 1024 / 3) / 2), yTerrain = 50;

        private void InitializeComponent()
        {    
            this.terrainControl = new TerrainControl();
            this.SuspendLayout();
         
            this.terrainControl.Dock = System.Windows.Forms.DockStyle.Fill; 
            this.terrainControl.Name = "terrainControl";
            this.terrainControl.TabIndex = 0;

            this.ClientSize = new System.Drawing.Size(widthFenetre, heightFenetre);
            this.Controls.Add(this.terrainControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Tennis";
            this.ResumeLayout(false);
        }
    }
}
