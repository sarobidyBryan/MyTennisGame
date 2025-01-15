using System;
using System.Drawing;
using System.Windows.Forms;

namespace TennisGame
{
    public class StartDialog : Form
    {
        public event Action? GameStarted;

        public StartDialog()
        { 
            this.Size = new Size(300, 150);
            this.BackColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;

            Label welcomeText = new Label
            {
                Text = "Bienvenue !",
                Font = new Font("Arial", 14, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 50,
            };

            Button startButton = new Button
            {
                Text = "Commencer le jeu",
                Font = new Font("Arial", 12),
                Dock = DockStyle.Bottom,
                Height = 40,
            };

            startButton.Click += (sender, e) =>
            {
                GameStarted?.Invoke();
                this.Close();
            };
            this.Controls.Add(welcomeText);
            this.Controls.Add(startButton);
        }
    }
}
