using System;
using System.Drawing;
using System.Windows.Forms;

namespace Assignment2Prj
{
    public partial class Game : Form
    {
        private readonly Timer _timer;
        private readonly Random _rng = new Random();

        private GameManager _manager;
        private Paddle _paddle;
        private Ball _ball;
        private Bricks _bricks;

        private int _verticalSpeed;
        private int _horizontalSpeed;
        private int _bricksRows;
        private int _bricksCols;
        private int _paddleSpeed;

        public Game()
        {
            InitializeComponent();

            _verticalSpeed = 5;
            _horizontalSpeed = 5;
            _bricksRows = 5;
            _bricksCols = 8;
            _paddleSpeed = 5;

            _ball = new Ball(_verticalSpeed, _horizontalSpeed);
            _paddle = new Paddle(_paddleSpeed) {
                Position = new PointF(Width / 2, Height - 80),
                Size = new Size(100, 20)
            };
            _bricks = new Bricks(_bricksRows, _bricksCols);

            _manager = new GameManager(_bricks, _ball, _paddle);

            _timer = new Timer { Interval = 1000 / 60 };
            _timer.Tick += (s, e) => Invalidate();
            _timer.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;

            g.Clear(Color.Black);

            _paddle.Render(g);
        }

        private void Game_Load(object sender, EventArgs e)
        {

        }
    }
}
