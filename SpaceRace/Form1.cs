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
        Rectangle player1 = new Rectangle(200, 360, 20, 20);
        Rectangle player2 = new Rectangle(200, 360, 20, 20);

        List<Rectangle> leftBalls = new List<Rectangle>();
        List<Rectangle> rightBalls = new List<Rectangle>();

        int ballsize = 10;
        int playerspeed = 10;
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
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
