using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

using Breakout.Audio;
using Breakout.Game;

namespace Breakout.Game
{
    public class BreakoutScreen : GameScreen
    {
        // UI
        private readonly PauseScreen _pauseScreen;
        private readonly ScoreOverlay _scoreOverlay;

        // State
        private bool _frozen = true;
        private bool _alive = true;
        private int _combo;

        // Objects
        private readonly Paddle _paddle;
        private readonly Ball _ball;
        private readonly List<Brick> _bricks;

        // Visual effects
        private readonly List<HitScore> _hitScores = new List<HitScore>();
        private readonly List<HitRing> _hitRings = new List<HitRing>();

        // Sounds
        private readonly ISound _hitSound;

        // Debugging
        private readonly Pen _debugPen = new Pen(Color.Red);
        private bool _debug, _isDraggingBall, _isSettingBallVelocity;
        private Vector2 _dragOffset;

        public bool IsPaused
        {
            get => _pauseScreen.IsPaused;
            set => _pauseScreen.IsPaused = value;
        }

        public int Score
        {
            get => _scoreOverlay.Value;
            set => _scoreOverlay.Value = value;
        }

        public BreakoutScreen(GameManager manager)
            : base(manager)
        {
            _pauseScreen = new PauseScreen(manager);

            // Initialize paddle, ball
            _paddle = new Paddle()
            {
                Position = new Vector2(Ui.ClientSize.Width / 2, Ui.ClientSize.Height - Theme.BrickUnit),
                Size = new Size(Theme.BrickUnit * 5 - Theme.GridUnit, Theme.BrickUnit * 1 - Theme.GridUnit)
            };

            _ball = new Ball(new Vector2(Ui.ClientSize.Width / 2, Ui.ClientSize.Height / 2))
            {
                Radius = Theme.GridUnit * 2,
                Color = Theme.FirmnessColors[0],
                Velocity = Vector2.Normalize(new Vector2(1.0f, 1.0f)) * 10.0f
            };

            // Initialize bricks
            _bricks = new List<Brick>();
            Vector2 brickSize = new Vector2(Theme.BrickUnit * 4, Theme.BrickUnit);

            int bricksWidth = Theme.BrickColumns * (int)brickSize.X;
            int padding = Theme.BrickUnit * 1;

            for (int row = 0; row < Theme.BrickRows; row++)
            {
                for (int col = 0; col < Theme.BrickColumns; col++)
                {
                    _bricks.Add(new Brick(
                        new Vector2(
                            padding + brickSize.X * col,
                            padding + brickSize.Y * row
                        ),
                        brickSize
                    )
                    { Firmness = 4 - row / 2 });
                }
            }

            // Load sounds
            _hitSound = Sound.Load(@"res\sound\tone1.wav");

            _scoreOverlay = new ScoreOverlay(Font, Ui, _paddle);
        }

        private void OnUiDeactivated(object sender, EventArgs e)
        {
            if (_alive)
                Pause();
        }

        protected override void OnAdd()
        {
            Cursor.Hide();

            Ui.Deactivate += OnUiDeactivated;
            Manager.AddScreen(_pauseScreen);
            Manager.AddScreen<FadeIn>(x => x.OnComplete = () => { _frozen = false; });
        }

        protected override void OnRemove()
        {
            Ui.Deactivate -= OnUiDeactivated;
            Manager.RemoveScreen(_pauseScreen);
        }

        #region Input
        protected override void OnMouseDown(MouseEventArgs e)
        {
            Vector2 mousePos = e.Location.ToVector2();

            if (_debug && IsPaused)
            {
                if (Vector2.Distance(mousePos, _ball.Position) <= _ball.Radius)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        _dragOffset = mousePos - _ball.Position;
                        _isDraggingBall = true;
                    }
                    else if (e.Button == MouseButtons.Right)
                    {
                        _isSettingBallVelocity = true;
                    }
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!_alive) return;

            Vector2 mousePos = e.Location.ToVector2();

            if (_debug)
            {
                if (_isDraggingBall)
                {
                    _ball.Position = e.Location.ToVector2() - _dragOffset;
                }
                else if (_isSettingBallVelocity)
                {
                    Vector2 vectorToMouse = mousePos - _ball.Position;
                    float angleToMouse = (float)Math.Atan2(vectorToMouse.Y, vectorToMouse.X);
                    _ball.Velocity = new Vector2((float)Math.Cos(angleToMouse), (float)Math.Sin(angleToMouse)) * 8.0f;
                }
            }

            if (IsPaused) return;

            _paddle.TargetPosition = new Vector2(e.X, _paddle.Position.Y);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (_isDraggingBall)
            {
                _isDraggingBall = false;
            }

            if (_isSettingBallVelocity)
            {
                _isSettingBallVelocity = false;
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (!_alive) return;

            switch (e.KeyCode)
            {
                case Keys.P:
                    {
                        if (IsPaused) Unpause();
                        else Pause();
                    }
                    break;
                case Keys.D:
                    {
                        _debug = !_debug;
                    }
                    break;
                case Keys.N:
                    {
                        if (!_debug || !IsPaused) return;
                        IsPaused = false;
                        Update();
                        IsPaused = true;
                    }
                    break;
                case Keys.R:
                    {
                        if (!_debug) return;
                        _ball.Position = new Vector2(Ui.ClientSize.Width, Ui.ClientSize.Height) / 2;
                    }
                    break;
                case Keys.Q:
                    {
                        Manager.RemoveScreen(this);
                    }
                    break;
            }
        }
        #endregion

        private void Pause()
        {
            if (IsPaused) return;

            IsPaused = true;
            Cursor.Clip = Rectangle.Empty;
            Cursor.Show();
        }

        private void Unpause()
        {
            if (!IsPaused) return;

            IsPaused = false;
            Cursor.Hide();
            Cursor.Clip = Ui.RectangleToScreen(Ui.ClientRectangle);
            Cursor.Position = Ui.PointToScreen(new Point((int)_paddle.Position.X, (int)Ui.ClientSize.Height / 2));
        }

        private void MoveBall(Ball ball, ref Vector2 v)
        {
            ball.Move(v);

            Vector2 ballVelocity = ball.Velocity;

            // Detect collisions
            RectangleF ballBounds = ball.Rect;

            if (ballBounds.IntersectsWith(_paddle.Bounds))
            {
                Vector2 paddleReflectOrigin = new Vector2(_paddle.Position.X, _paddle.Position.Y + 40);
                float normalAngle = (float)Math.Atan2(
                    ball.Position.Y - paddleReflectOrigin.Y,
                    ball.Position.X - paddleReflectOrigin.X
                );
                Vector2 paddleReflectionNormal = new Vector2(
                    (float)Math.Cos(normalAngle),
                    (float)Math.Sin(normalAngle)
                );
                ball.Velocity = paddleReflectionNormal * ball.Velocity.Length();
            }

            if ((ball.Velocity.X < 0 && ball.Position.X < ball.Radius) ||
                (ball.Velocity.X > 0 && ball.Position.X > (Ui.ClientSize.Width - ball.Radius)))
            {
                ball.Velocity *= new Vector2(-1, 1);
            }

            if (ball.Velocity.Y < 0 && ball.Position.Y < ball.Radius)
            {
                ball.Velocity *= new Vector2(1, -1);
            }

            float maxCollisionArea = 0;
            Brick collisionBrick = null;
            RectangleF totalIntersection = default;
            for (int i = 0; i < _bricks.Count; i++)
            {
                Brick brick = _bricks[i];

                RectangleF bounds = brick.Bounds;
                RectangleF intersection = RectangleF.Intersect(ballBounds, bounds);
                if (intersection.IsEmpty) continue;

                float area = intersection.Width * intersection.Height;
                if (area > maxCollisionArea)
                {
                    maxCollisionArea = area;
                    collisionBrick = brick;
                }

                if (totalIntersection.IsEmpty)
                {
                    totalIntersection = intersection;
                }
                else
                {
                    totalIntersection = RectangleF.Union(intersection, totalIntersection);
                }

            }

            if (collisionBrick != null)
            {
                HandleCollision(ball, collisionBrick);

                if (totalIntersection.Width > totalIntersection.Height)
                {
                    ball.Velocity *= new Vector2(1, -1);
                }
                else
                {
                    ball.Velocity *= new Vector2(-1, 1);
                }
            }

            if (ballVelocity != ball.Velocity)
            {
                Sound.Play(_hitSound);
            }
        }

        private void HandleCollision(Ball ball, Brick brick)
        {
            Color color = Theme.FirmnessColors[brick.Firmness];

            if (ball.Color == color)
            {
                _combo++;
            }
            else
            {
                // Reset combo
                ball.Color = color;
                _combo = 0;
                _hitRings.Add(new HitRing(ball.Position, ball.Radius, color));
            }

            int additionalScore = 10;

            if (--brick.Firmness == 0)
            {
                _bricks.Remove(brick);
                additionalScore += 5;
            }

            additionalScore += _combo;
            Score += additionalScore;

            _hitScores.Add(new HitScore(
                Font,
                brick.Position + brick.Size / 2,
                $"{additionalScore}",
                Theme.FirmnessColors[brick.Firmness + 1]
            ));
        }

        protected override void OnUpdate()
        {
            if (IsPaused) return;

            // Update visual elements (hit scores & hit rings)
            for (int i = 0; i < _hitScores.Count; i++)
                if (!_hitScores[i].Update())
                    _hitScores.RemoveAt(i--);

            for (int i = 0; i < _hitRings.Count; i++)
                if (!_hitRings[i].Update())
                    _hitRings.RemoveAt(i--);

            _scoreOverlay.Update();

            if (!_alive) return;

            if (!_frozen)
            {
                Vector2 v = _ball.Velocity;
                MoveBall(_ball, ref v);

                if (_ball.Position.Y > (Ui.ClientSize.Height + 100))
                {
                    _alive = false;
                    Cursor.Show();
                    Manager.AddScreen(new GameOverScreen(Manager));
                }
            }

            _paddle.Update();
        }

        protected override void OnDraw(Graphics g)
        {
            foreach (var brick in _bricks)
                brick.Draw(g);

            _paddle.Draw(g);

            _ball.Draw(g);

            foreach (var ring in _hitRings)
                ring.Draw(g);

            foreach (var score in _hitScores)
                score.Draw(g);

            _pauseScreen.Draw(g);
            _scoreOverlay.Draw(g);

            if (_debug)
            {
                Vector2 velocity = Vector2.Normalize(_ball.Velocity);
                Vector2 origin = _ball.Position + velocity * _ball.Radius;
                g.DrawLine(_debugPen, origin.ToPointF(), (origin + velocity * Theme.GridUnit * 10).ToPointF());
            }
        }
    }
}