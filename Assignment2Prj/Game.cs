using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment2Prj
{
    public partial class Game : Form
    {
        /// <summary>
        /// 
        /// </summary>
        /// 

        private Bricks bricks;
        private Ball ball;
        private Paddle paddle;
        private Mannger mannger;

        private int verticalSpeed;
        private int horizontalSpeed;
        private int bricksRows;
        private int bricksCols;
        private int paddleSpeed;





        public Game()
        {
            InitializeComponent();
            //Adjust these numbers 
            this.verticalSpeed = 5;
            this.horizontalSpeed = 5;
            this.bricksRows = 5;
            this.bricksCols = 8;
            this.paddleSpeed = 5;

            //Create objects 
            this.ball = new Ball(picBall, verticalSpeed, horizontalSpeed);
            this.paddle = new Paddle(picPaddle, paddleSpeed);
            this.bricks = new Bricks(bricksRows, bricksCols);

            this.mannger = new Mannger(bricks, ball, paddle);

        }

        private void Game_Load(object sender, EventArgs e)
        {

        }
    }
}
