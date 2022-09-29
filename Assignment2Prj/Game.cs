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

        private Bricks _bricks;
        private Ball _ball;
        private Paddle _paddle;
        private GameManager _manager;

        private int _verticalSpeed;
        private int _horizontalSpeed;
        private int _bricksRows;
        private int _bricksCols;
        private int _paddleSpeed;





        public Game()
        {
            InitializeComponent();
            //Adjust these numbers 
            _verticalSpeed = 5;
            _horizontalSpeed = 5;
            _bricksRows = 5;
            _bricksCols = 8;
            _paddleSpeed = 5;

            //Create objects 
            _ball = new Ball(picBall, _verticalSpeed, _horizontalSpeed);
            _paddle = new Paddle(picPaddle, _paddleSpeed);
            _bricks = new Bricks(_bricksRows, _bricksCols);

            _manager = new GameManager(_bricks, _ball, _paddle);

        }

        private void Game_Load(object sender, EventArgs e)
        {

        }
    }
}
