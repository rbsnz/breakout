using System;
using System.Drawing;
using System.Numerics;
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
                Velocity = Vector2.Normalize(new Vector2(1.0f, 1.0f)) * 4.0f
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
            int steps = (int)Math.Ceiling(_ball.Velocity.Length());
            for (int i = 0; i < steps; i++)
                BallStep(1.0f / steps);

            if (_ball.Position.Y > (_stageArea.Height + 100))
            {
                _ball.Position = new PointF(_stageArea.Width / 2, _stageArea.Height / 2);
            }
        }

        private void BallStep(float fraction)
        {
            _ball.Position = new PointF(
                _ball.Position.X + _ball.Velocity.X * fraction,
                _ball.Position.Y + _ball.Velocity.Y * fraction
            );
            Vector2 v2 = _ball.Velocity;
            if ((v2.Y > 0 && _ball.Rect.IntersectsWith(_paddle.Rect)))
                v2 = Vector2.Reflect(v2, new Vector2(0, -1));
            if ((v2.Y < 0 && _ball.Rect.IntersectsWith(_topEdge)))
                v2 = Vector2.Reflect(v2, new Vector2(0, 1));
            if (v2.X < 0 && _ball.Rect.IntersectsWith(_leftEdge))
                v2 = Vector2.Reflect(v2, new Vector2(1, 0));
            if (v2.X > 0 && _ball.Rect.IntersectsWith(_rightEdge))
                v2 = Vector2.Reflect(v2, new Vector2(-1, 0));
            _ball.Velocity = v2;
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
