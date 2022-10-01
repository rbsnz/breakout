using System;
using System.Drawing;
using System.Windows.Forms;

namespace Assignment2Prj
{
    class GameManager : IRenderable
    {
        private readonly Size _stageSize;

        private readonly RectangleF
            _stageArea, _leftEdge, _rightEdge, _topEdge, _bottomEdge;

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
            _stageArea = new RectangleF(0, 0, _stageSize.Width, _stageSize.Height);
            _leftEdge = new RectangleF(-100, 0, 100, _stageSize.Height);
            _rightEdge = new RectangleF(_stageSize.Width, 0, 100, _stageSize.Height);
            _topEdge = new RectangleF(0, -100, _stageSize.Width, 100);
            _bottomEdge = new RectangleF(0, _stageSize.Height, _stageSize.Width, 100);

            _verticalSpeed = 5;
            _horizontalSpeed = 5;
            _bricksRows = 5;
            _bricksCols = 8;
            _paddleSpeed = 5;

            _bricks = new Bricks(_bricksRows, _bricksCols);
            _ball = new Ball()
            {
                Position = new PointF(_stageSize.Width / 2, _stageSize.Height / 2),
                Velocity = 12
            };
            _paddle = new Paddle(_paddleSpeed)
            {
                Position = new PointF(_stageSize.Width / 2, _stageSize.Height - 18),
                Size = new Size(100, 16)
            };
        }

        public void HandleMouseMove(MouseEventArgs e)
        {
            _paddle.Position = new PointF(e.X, _paddle.Position.Y);
        }

        public void Update()
        {
            int steps = (int)Math.Ceiling(_ball.Velocity);
            for (int i = 0; i < steps; i++)
                BallStep(1.0f / steps);
        }

        private void BallStep(float fraction)
        {
            float velocity = _ball.Velocity * fraction;
            double angle = Math.Atan2(_ball.Direction.X, _ball.Direction.Y);
            _ball.Position = new PointF(
                _ball.Position.X + (float)Math.Sin(angle) * _ball.Velocity * fraction,
                _ball.Position.Y + (float)Math.Cos(angle) * _ball.Velocity * fraction
            );
            PointF dir = _ball.Direction;
            if ((dir.Y > 0 && _ball.Rect.IntersectsWith(_paddle.Rect)) ||
                (dir.Y < 0 && _ball.Rect.IntersectsWith(_topEdge)))
            {
                dir.Y *= -1;
            }
            if ((dir.X < 0 && _ball.Rect.IntersectsWith(_leftEdge)) ||
                (dir.X > 0 && _ball.Rect.IntersectsWith(_rightEdge)))
            {
                dir.X *= -1;
            }
            _ball.Direction = dir;
        }

        public void Render(Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            g.Clear(Color.Black);

            _paddle.Render(g);
            _ball.Render(g);
        }
    }
}
