using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dowierocks
{
    public partial class Form1 : Form
    {
        private Circle food = new Circle();
        private List<Circle> Snake = new List<Circle>();


        public Form1()
        {
            InitializeComponent();
            new Settings();
            label3.Visible = false;

            gameTimer.Interval = 2000 / Settings.Speed;
            gameTimer.Tick += UpdateScreen;
            gameTimer.Start();

            StartGame();

        }

        private void StartGame()
        {
            label3.Visible = false;
            new Settings();
            Snake.Clear();
            Circle head = new Circle { X = 10, Y = 5 };
            Snake.Add(head);
            label2.Text = Settings.Score.ToString();
            GenerateFood();
        }




        public void GenerateFood()
        {
            int maxX = pbcanvas.Size.Width / Settings.Width;
            int maxY = pbcanvas.Size.Height / Settings.Height;
            Random rnd = new Random();

            food = new Circle { X = rnd.Next(0, maxX), Y = rnd.Next(0, maxY) };
        }

        public void Die()
        {
            Settings.GameOver = true;
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, true);
            if (Settings.GameOver == true)
            {
                StartGame();
            }
        }



        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, false);
        }

        public void MovePlayer()
        {
            for (int i = Snake.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    switch (Settings.Direction)
                    {
                        case Direction.Right:
                            Snake[i].X++;
                            break;
                        case Direction.Left:
                            Snake[i].X--;
                            break;
                        case Direction.Up:
                            Snake[i].Y--;
                            break;
                        case Direction.Down:
                            Snake[i].Y++;
                            break;


                    }
                    int maxXposition = pbcanvas.Size.Width / Settings.Width;
                    int maxYposition = pbcanvas.Size.Height / Settings.Height;

                    if (Snake[i].X < 0 || Snake[i].Y < 0 || Snake[i].X > maxXposition || Snake[i].Y > maxYposition)
                    {
                        Die();
                    }
                    for (int j = 1; j < Snake.Count; j++)
                    {
                        if (Snake[i].X == Snake[j].X && Snake[i].Y == Snake[j].Y)
                        {
                            Die();
                        }
                      

                    }
                    if (Snake[0].X == food.X && Snake[0].Y == food.Y)
                    {
                        Eat();
                    }
                }

                else
                {
                    Snake[i].X = Snake[i - 1].X;
                    Snake[i].Y = Snake[i - 1].Y;
                }

            }
        }


        private void Eat()
        {
            Circle body = new Circle
            {
                X = Snake[Snake.Count - 1].X,
                Y = Snake[Snake.Count - 1].Y
            };
            Snake.Add(body);
            Settings.Score += Settings.Points;
            label2.Text = Settings.Score.ToString();
            GenerateFood();

        }
        private void UpdateGraphics(object sender, PaintEventArgs e)
        {
            if (Settings.GameOver == false)
            {
                //LinearGradientBrush linGrBrush = new LinearGradientBrush(
                //    new Point(0, 10),
                //    new Point(20, 10),
                //    Color.FromArgb(255, 0, 0, 0),
                //    Color.FromArgb(255, 245, 225, 16)); 
                //linear gradient definition

                Brush snakeColor;
                for (int i = 0; i < Snake.Count; i++)
                {

                    GraphicsPath path = new GraphicsPath();
                    path.AddEllipse(Snake[i].X * Settings.Width, Snake[i].Y * Settings.Height,
                        Settings.Width,
                        Settings.Height);
                    // Use the path to construct a brush.
                    PathGradientBrush pthGrBrush = new PathGradientBrush(path);
                    // Set the color at the center of the path to blue.
                    pthGrBrush.CenterColor = Color.Black;
                    Color[] colors = { Color.FromArgb(255, 245, 225, 16) };
                    pthGrBrush.SurroundColors = colors;

                    if (i == 0)
                    { snakeColor =   Brushes.Black; }
                    else
                    {
                        // snakeColor = linGrBrush;
                        // for linear gradient
                        snakeColor = pthGrBrush;
                    }

                    e.Graphics.FillEllipse(snakeColor, 
                        new Rectangle(Snake[i].X * Settings.Width, Snake[i].Y * Settings.Height, 
                        Settings.Width, 
                        Settings.Height));
                    e.Graphics.FillEllipse(Brushes.Red, 
                        new Rectangle(food.X * Settings.Width, food.Y * Settings.Height, 
                        Settings.Width, 
                        Settings.Height));
                }
            }
            else
            {
                string GameOver = "You aliven't! Skorrr: " + Settings.Score;
                label3.Text = GameOver;
                label3.Visible = true;
            }
        }

        private void UpdateScreen(object sender, EventArgs e)
       
            {
                if (Input.KeyPress(Keys.Right) && Settings.Direction != Direction.Left)
                {
                    Settings.Direction = Direction.Right;
                }
                else if (Input.KeyPress(Keys.Up) && Settings.Direction != Direction.Down)
                {
                    Settings.Direction = Direction.Up;
                }
                else if (Input.KeyPress(Keys.Down) && Settings.Direction != Direction.Up)
                {
                    Settings.Direction = Direction.Down;
                }
                else if (Input.KeyPress(Keys.Left) && Settings.Direction != Direction.Right)
                {
                    Settings.Direction = Direction.Left;
                }
                MovePlayer();
              pbcanvas.Invalidate();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

    }

}
