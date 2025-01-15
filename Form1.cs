using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using Timer = System.Windows.Forms.Timer;

namespace TennisGame
{
    public partial class Form1 : Form
    {
        private List<Shape> shapes = new();
        private List<Shape> cibles = new();
        private CibleMovementService cibleMovementService;
        private ShapeMovementService movementService;
        private KeyboardManager keyboardManager = new();
        private Timer timer;
        private TargetIndicator targetIndicator;
        private TennisGameManager gameManager;
        public Match Match { get; set; }
        public Ball gameball { get; set; }
        private DataGridView scoreTable;

        public Form1()
        {
            Rectangle rect = new Rectangle(xTerrain, yTerrain, widthTerrain, heightTerrain);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            InitializeComponent();

            scoreTable = new DataGridView();
            this.Controls.Add(scoreTable);

            StartDialog startDialog = new StartDialog();
            startDialog.GameStarted += () =>
            {
                ShowInputDialog();
            };

            startDialog.ShowDialog(this);
        }

        private void ShowInputDialog()
        {
            Form inputForm = new Form
            {
                Width = 300,
                Height = 200,
                Text = "Parametres"
            };

            Label label1 = new Label { Left = 10, Top = 20, Text = "Vitesse du cible 1" };
            TextBox textBox1 = new TextBox { Left = 150, Top = 20, Width = 100 };

            Label label2 = new Label { Left = 10, Top = 60, Text = "Point obtenu au cible 2" };
            TextBox textBox2 = new TextBox { Left = 150, Top = 60, Width = 100 };

            Button confirmation = new Button { Text = "Ok", Left = 150, Width = 100, Top = 100 };
            confirmation.Click += (sender, e) =>
            {
                if (int.TryParse(textBox1.Text, out int speedTraj1) && int.TryParse(textBox2.Text, out int pointCible2))
                {
                    int speedTraj2 = speedTraj1 * pointCible2;
                    InitializeShapes(speedTraj1, pointCible2, speedTraj2);
                    InitializeTerrain();
                    InitializeTargetIndicator();
                    InitializeKeyboard();

                    Rectangle rect = new Rectangle(xTerrain, yTerrain, widthTerrain, heightTerrain);
                    gameManager = new TennisGameManager(rect, (Player)shapes[0], (Player)shapes[1], gameball, targetIndicator, Match, scoreTable);

                    movementService = new ShapeMovementService(shapes);
                    cibleMovementService = new CibleMovementService(cibles);
                    timer = new Timer { Interval = 50 };
                    timer.Tick += Timer_Tick;

                    timer.Start();

                    this.KeyDown += Form1_KeyDown;
                    this.KeyUp += Form1_KeyUp;
                    this.Paint += new PaintEventHandler(Form1_Paint);
                    this.Invalidate();

                    inputForm.Close();
                }
                else
                {
                    MessageBox.Show("Please enter valid integer values.");
                }
            };

            inputForm.Controls.Add(label1);
            inputForm.Controls.Add(textBox1);
            inputForm.Controls.Add(label2);
            inputForm.Controls.Add(textBox2);
            inputForm.Controls.Add(confirmation);
            inputForm.ShowDialog();
        }

        private void InitializeTargetIndicator()
        {
            Console.WriteLine("initTargetIndicator");
            int xPosition = xTerrain + widthTerrain + 50;
            int yPosition = yTerrain;
            int minValue = xTerrain; 
            int maxValue = xTerrain + widthTerrain;
            int width = 100; 
            int height = 50; 

            targetIndicator = new TargetIndicator(xPosition, yPosition, minValue, maxValue, width, height);
        }

        private void InitializeTerrain()
        {
            int terrainWidth = widthTerrain;
            int terrainHeight = heightTerrain;
            int terrainX = xTerrain;
            int terrainY = yTerrain;

            Terrain terrain = new Terrain(terrainX, terrainY, terrainWidth, terrainHeight);
            terrainControl.SetTerrain(terrain);
            terrainControl.SetShapes(shapes);
        }

        public void InitializeShapes(int speedTraj1, int pointCible2, int speedTraj2)
        {
            Console.WriteLine("initShapes");
           
            Player player1 = new Player(
                "Player1",
                xTerrain + (widthTerrain / 2) - 25, 
                yTerrain + 150,                     
                100,                                
                10,                               
                Color.Blue                        
            );

            
            Player player2 = new Player(
                "Player2",
                xTerrain + (widthTerrain / 2) - 25, 
                yTerrain + heightTerrain - 150,    
                100,                               
                10,                               
                Color.Magenta                     
            );

            Match = new Match(player1, player2);

            Trajectory player1Traj1 = new LineTrajectory(new Point(xTerrain, yTerrain + 100), new Point(xTerrain + widthTerrain, yTerrain + 100), speedTraj1);
            Trajectory player1Traj2 = new LineTrajectory(new Point(xTerrain, yTerrain + 50), new Point(xTerrain + widthTerrain, yTerrain + 50), speedTraj2);
            Trajectory player2Traj1 = new LineTrajectory(new Point(xTerrain, yTerrain + heightTerrain - 100), new Point(xTerrain + widthTerrain, yTerrain + heightTerrain - 100), speedTraj1);
            Trajectory player2Traj2 = new LineTrajectory(new Point(xTerrain, yTerrain + heightTerrain - 50), new Point(xTerrain + widthTerrain, yTerrain + heightTerrain - 50), speedTraj2);

            Ball player1Cible1 = new Cible(xTerrain, yTerrain + 100, 20, Color.Red, player1Traj1, 1);
            Ball player1Cible2 = new Cible(xTerrain, yTerrain + 50, 20, Color.Red, player1Traj2, pointCible2);
            Ball player2Cible1 = new Cible(xTerrain, yTerrain + heightTerrain - 100, 20, Color.Red, player2Traj1, 1);
            Ball player2Cible2 = new Cible(xTerrain, yTerrain + heightTerrain - 50, 20, Color.Red, player2Traj2, pointCible2);

          
            gameball = new Ball(player1.X, player1.Y, 20, Color.GreenYellow, null);

         
            shapes.Add(player1);
            shapes.Add(player2);

            shapes.Add(player1Cible1);
            shapes.Add(player1Cible2);
            shapes.Add(player2Cible1);
            shapes.Add(player2Cible2);

            shapes.Add(gameball);
        }
        private void InitializeKeyboard()
        {
            Console.WriteLine("key init");
            keyboardManager.AddKeyMapping(Keys.NumPad4, -1, 0);
            keyboardManager.AddKeyMapping(Keys.NumPad6, 1, 0);

            keyboardManager.AddKeyMapping(Keys.A, -1, 0);
            keyboardManager.AddKeyMapping(Keys.D, 1, 0);
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            Rectangle rect = new Rectangle(xTerrain, yTerrain, widthTerrain, heightTerrain);

            foreach (var shape in shapes)
            {
                shape.Move(rect);
            }

            gameManager.UpdateGameState(shapes);
            movementService.UpdateMovement(rect);
            cibleMovementService.UpdateMovement(rect);
            terrainControl.SetShapes(shapes);

            this.Invalidate();
        }

        void Form1_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                shapes[0].UpdateDirection(-1, 0);
            }
            else if (e.KeyCode == Keys.D)
            {
                shapes[0].UpdateDirection(1, 0);
            }

            if (e.KeyCode == Keys.NumPad4)
            {
                shapes[1].UpdateDirection(-1, 0);
            }
            else if (e.KeyCode == Keys.NumPad6)
            {
                shapes[1].UpdateDirection(1, 0);
            }

            if (e.KeyCode == Keys.V)
            {
                targetIndicator.MoveLeft();
            }
            else if (e.KeyCode == Keys.N)
            {
                targetIndicator.MoveRight();
            }

            if (e.KeyCode == Keys.Escape)
            {
                Restart();
            }

            if (e.KeyCode == Keys.L)
            {
                var dbConnection = new Connexion("localhost", "tennis", "postgres", " ");
                var scoreHistories = dbConnection.GetScoreHistories();

                var fileWriter = new FileWriter();
                fileWriter.WriteScoreHistoriesToFile(scoreHistories, "score_history.txt");
            }

            this.Invalidate();
        }

        private void Form1_KeyUp(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.D)
            {
                shapes[0].UpdateDirection(0, 0);
            }

            if (e.KeyCode == Keys.NumPad4 || e.KeyCode == Keys.NumPad6)
            {
                shapes[1].UpdateDirection(0, 0);
            }
        }

        private void Form1_Paint(object? sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            Console.WriteLine("OnPaint called.");

            foreach (var shape in shapes)
            {
                shape.Draw(e.Graphics);
            }

            targetIndicator?.Draw(e.Graphics);
        }

        private string GetScoreText(int score)
        {
            return score switch
            {
                0 => "0",
                15 => "15",
                30 => "30",
                40 => "40",
                _ => "Unknown",
            };
        }

        private void Restart()
        {
            timer.Stop();

            shapes.Clear();
            cibles.Clear();
            ShowInputDialog();

            InitializeTerrain();

            InitializeTargetIndicator();

            Rectangle rect = new Rectangle(xTerrain, yTerrain, widthTerrain, heightTerrain);
            gameManager = new TennisGameManager(rect, (Player)shapes[0], (Player)shapes[1], gameball, targetIndicator, Match, scoreTable);

            timer.Start();

            this.Invalidate();
        }
    }
}
