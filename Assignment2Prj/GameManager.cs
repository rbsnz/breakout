using System;
using System.Drawing;
using System.Windows.Forms;

namespace Assignment2Prj
{
    class GameManager : IRenderable
    {
        private readonly Size _stageSize;
        private readonly Bricks _bricks;
        private readonly Ball _ball;
        private readonly Paddle _paddle;

        private int _verticalSpeed;
        private int _horizontalSpeed;
        private int _bricksRows;
        private int _bricksCols;
        private int _paddleSpeed;

        public GameManager(Size stageSize)
        {
            _stageSize = stageSize;

            _verticalSpeed = 5;
            _horizontalSpeed = 5;
            _bricksRows = 5;
            _bricksCols = 8;
            _paddleSpeed = 5;

            _bricks = new Bricks(_bricksRows, _bricksCols);
            _ball = new Ball(_verticalSpeed, _horizontalSpeed);
            _paddle = new Paddle(_paddleSpeed)
            {
                Position = new PointF(_stageSize.Width / 2, _stageSize.Height - 40),
                Size = new Size(100, 20)
            };
        }

        public void HandleMouseMove(MouseEventArgs e)
        {
            _paddle.Position = new PointF(e.X, _paddle.Position.Y);
        }

        public void Update()
        {

        }

        public void Render(Graphics g)
        {
            g.Clear(Color.Black);

            _paddle.Render(g);
        }
    }
}
