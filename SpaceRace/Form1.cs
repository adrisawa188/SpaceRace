using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceRace
{
    public partial class Form1 : Form
    {
        //global variables
        Rectangle player1;
        Rectangle player2; 

        List<Rectangle> leftBalls = new List<Rectangle>();
        List<Rectangle> rightBalls = new List<Rectangle>();

        int ballSize = 10;
        int ballSpeed = 5;
        int playerSpeed = 4;
        int p1Score = 0;
        int p2Score = 0;

        bool p1Up = false;
        bool p1Down = false;
        bool p2Up = false;
        bool p2Down = false;

        SolidBrush whiteBrush = new SolidBrush(Color.White);

        Random randGen = new Random();

        int randValue = 0;

        string gameState = "waiting";

        public Form1()
        {
            InitializeComponent();
            //startButton.Enabled = false;
            //startButton.Visible = false;
            //quitButton.Enabled = false;
            //quitButton.Visible = false; 
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    p1Up = true;
                    break;
                case Keys.Up:
                    p2Up = true;
                    break;
                case Keys.S:
                    p1Down = true;
                    break;
                case Keys.Down:
                    p2Down = true;
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    p1Up = false;
                    break;
                case Keys.Up:
                    p2Up = false;
                    break;
                case Keys.S:
                    p1Down = false;
                    break;
                case Keys.Down:
                    p2Down = false;
                    break;
            }
        }

            private void gameTimer_Tick(object sender, EventArgs e)
            {
            //move players
            movePlayer();

            //generate and move left obstacles
            Obstacles();

            //remove balls when they touch opposite walls
            obstacleWallCollision();

            //player collision with balls 
            playerBallCollision();

            //player collision with top wall
            playerWinWall();

            //change the gamestate to over and diplay witch player won
            playerWin();
            
            Refresh();
            }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (gameState == "waiting")
            {
                outputLabel.Text = "Space Race";
                startButton.Enabled = true;
                quitButton.Enabled = true;
                startButton.Visible = true;
                quitButton.Visible = true;
            }
            else if (gameState == "running")
            {
                startButton.Enabled = false;
                quitButton.Enabled = false;
                startButton.Visible = false;
                quitButton.Visible = false;

                e.Graphics.FillRectangle(whiteBrush, player1);
                e.Graphics.FillRectangle(whiteBrush, player2);
                e.Graphics.FillRectangle(whiteBrush, 290, 0, 10, 400);

                for (int i = 0; i < leftBalls.Count(); i++)
                {
                    e.Graphics.FillEllipse(whiteBrush, leftBalls[i]);
                }

                for (int i = 0; i < rightBalls.Count(); i++)
                {
                    e.Graphics.FillEllipse(whiteBrush, rightBalls[i]);
                }
            }
            else if (gameState == "over")
            {
                gameTimer.Enabled = false;

                startButton.Enabled = true;
                quitButton.Enabled = true;
                startButton.Visible = true;
                quitButton.Visible = true;

                p1Score = 0;
                p2Score = 0;

                p1ScoreLabel.Text =$"{p1Score}";
                p2ScoreLabel.Text =$"{p2Score}";

                startButton.Text = "Play Again?";

            }
        }

            private void startButton_Click(object sender, EventArgs e)
            {
            gameState = "running";
            gameStart();
            }

            private void quitButton_Click(object sender, EventArgs e)
            {
            Application.Exit(); 
            }

        public void movePlayer()
        {
            if (p1Up == true && player1.Y > 0)
            {
                player1.Y -= playerSpeed;
            }

            if (p1Down == true && player1.Y < this.Height - player1.Height)
            {
                player1.Y += playerSpeed;
            }
            if (p2Up == true && player2.Y > 0)
            {
                player2.Y -= playerSpeed;
            }

            if (p2Down == true && player2.Y < this.Height - player1.Height)
            {
                player2.Y += playerSpeed;
            }

        }  

        public void Obstacles()
        {
            randValue = randGen.Next(0, 101);

            //generat new balls
            if (randValue <10)
            {
                int x = randGen.Next(10, this.Height - 60);
                leftBalls.Add(new Rectangle(0, x, ballSize, ballSize));               
            }

            //move balls
            for (int i = 0; i < leftBalls.Count(); i++)
            {
                int y = leftBalls[i].X + ballSpeed; 
                leftBalls[i] = new Rectangle(y, leftBalls[i].Y, ballSize, ballSize);
            }

            //generat new balls
            if (randValue < 10)
            {
                int x = randGen.Next(10, this.Height - 60);
                rightBalls.Add(new Rectangle(600 - ballSize, x, ballSize, ballSize));
            }

            //move balls
            for (int i = 0; i < rightBalls.Count(); i++)
            {
                int y = rightBalls[i].X - ballSpeed;
                rightBalls[i] = new Rectangle(y, rightBalls[i].Y, ballSize, ballSize);
            }

        }

        public void obstacleWallCollision()
        {
            for (int i = 0; i < leftBalls.Count(); i++)
            {
                if (leftBalls[i].X > this.Width)
                {
                    leftBalls.RemoveAt(i);                   
                }
            }

            for (int i = 0; i < rightBalls.Count(); i++)
            {
                if (rightBalls[i].X > this.Width)
                {
                    rightBalls.RemoveAt(i);
                }
            }
        }

        public void playerBallCollision() 
        {
            for (int i = 0; i < leftBalls.Count(); i++)
            {
                if (player1.IntersectsWith(leftBalls[i]))
                {
                    player1.Y = 360;
                }
                else if (player2.IntersectsWith(leftBalls[i]))
                {
                    player2.Y = 360;
                }
            }

            for (int i = 0; i < rightBalls.Count(); i++)
            {
                if (player1.IntersectsWith(rightBalls[i]))
                {
                    player1.Y = 360;
                }
                else if (player2.IntersectsWith(rightBalls[i]))
                {
                    player2.Y = 360; 
                }
            }                      
        }

        public void playerWinWall()
        {
            
            if (player1.Y < 1)
             {
                p1Score++;
                p1ScoreLabel.Text = $"{p1Score}";
                player1.Y = 360;
             }
            else if (player2.Y < 1)
            {
                p2Score++;
                p2ScoreLabel.Text = $"{p2Score}";
                player2.Y = 360;
            }

        }

        public void playerWin()
        {
            if (p1Score == 5)
            {
                gameState = "over";
                outputLabel.Text = "Player 1 Wins!!";
            }
            else if (p2Score == 5)
            {
                gameState = "over";
                outputLabel.Text = "Player 2 Wins!!";
            }
        }

        public void gameStart()
        {
            outputLabel.Text = "";
            gameState = "running";

            p1ScoreLabel.Text =$"{p1Score}";
            p2ScoreLabel.Text = $"{p2Score}";

            player1 = new Rectangle(180, 360, 20, 20);
            player2 = new Rectangle(380, 360, 20, 20);

            leftBalls.Clear();
            leftBalls.Add(new Rectangle(0, 0, ballSize, ballSize));
            rightBalls.Clear();
            rightBalls.Add(new Rectangle(600 - ballSize, 0, ballSize, ballSize));

            gameTimer.Enabled = true;
        }
    }
}
