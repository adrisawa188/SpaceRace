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
        Rectangle player1 = new Rectangle(180, 360, 20, 20);
        Rectangle player2 = new Rectangle(380, 360, 20, 20);

        List<Rectangle> leftBalls = new List<Rectangle>();
        List<Rectangle> rightBalls = new List<Rectangle>();

        int ballSize = 10;
        int ballSpeed = 8;
        int playerSpeed = 6;
        int p1Score = 0;
        int p2Score = 0;

        bool p1Up = false;
        bool p1Down = false;
        bool p2Up = false;
        bool p2Down = false;

        SolidBrush whiteBrush = new SolidBrush(Color.White);

        Random randGen = new Random();

        int randValue = 0;

        string gameState;

        public Form1()
        {
            InitializeComponent();
            startButton.Enabled = false;
            startButton.Visible = false;
            quitButton.Enabled = false;
            quitButton.Visible = false; 
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
     
            Refresh();
            }

            private void Form1_Paint(object sender, PaintEventArgs e)
            {
            e.Graphics.FillRectangle(whiteBrush, player1);
            e.Graphics.FillRectangle(whiteBrush, player2);

            }

            private void startButton_Click(object sender, EventArgs e)
            {

            }

            private void quitButton_Click(object sender, EventArgs e)
            {

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

        public void leftObstacles()
        {
            randValue = randGen.Next(0, 101);

            //generat new balls
            if (randValue <15)
            {
                int x = randGen.Next(10, this.Width - ballSize * 2);
                leftBalls.Add(new Rectangle(x, 0, ballSize, ballSize));               
            }

            for (int i = 0; i < leftBalls.Count(); i++)
            {
                int y = leftBalls[i].X + ballSpeed;
                leftBalls[i] = new Rectangle(leftBalls[i].X, y, ballSize, ballSize);
            }


        }
    }
}
