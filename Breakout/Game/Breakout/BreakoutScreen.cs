using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

using Breakout.Audio;

namespace Breakout.Game
{
    /// <summary>
    /// Handles all logic for the breakout game.
    /// </summary>
    public class BreakoutScreen : GameScreen
    {
        private bool _transitioning;
        private bool _isMouseCaptured;

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

        /// <summary>
        /// Gets or sets whether the game is paused.
        /// </summary>
        public bool IsPaused
        {
            get => _pauseScreen.IsPaused;
            set => _pauseScreen.IsPaused = value;
        }

        /// <summary>
        /// Gets or sets the score.
        /// </summary>
        public int Score
        {
            get => _scoreOverlay.Value;
            set => _scoreOverlay.Value = value;
        }

        /// <summary>
        /// Constructs a new breakout screen.
        /// </summary>
        public BreakoutScreen(GameManager manager)
            : base(manager)
        {

            // Initialize the paddle & ball.
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

            // Initialize the bricks.
            _bricks = new List<Brick>();
            Vector2 brickSize = new Vector2(Theme.BrickUnit * 4, Theme.BrickUnit);

            int padding = Theme.BrickUnit * 1;

            for (int row = 0; row < Theme.BrickRows; row++)
            {
                for (int col = 0; col < Theme.BrickColumns; col++)
                {
                    // Add a brick and set the firmness based on its row.
                    Vector2 brickPos = new Vector2(padding + brickSize.X * col, padding + brickSize.Y * row);
                    _bricks.Add(new Brick(brickPos, brickSize) { Firmness = 4 - row / 2 });
                }
            }

            // Load sounds.
            _hitSound = Sound.Load(@"res\sound\tone1.wav");

            // Initialize the score and pause overlays.
            _scoreOverlay = new ScoreOverlay(Manager, _paddle);
            _pauseScreen = new PauseScreen(manager);
        }

        // Initializes the screen when it is added.
        protected override void OnAdd()
        {
            Ui.Deactivate += OnUiDeactivated;
            AddScreen(_pauseScreen);
            AddFadeIn(() => _frozen = false);

            CaptureMouse();
        }

        // Cleans up resources when the screen is removed.
        protected override void OnRemove()
        {
            Ui.Deactivate -= OnUiDeactivated;
            RemoveScreen(_pauseScreen);
        }

        // Pauses the game when the window loses focus.
        private void OnUiDeactivated(object sender, EventArgs e)
        {
            if (_alive && !_debug) Pause();
        }

        #region Input
        protected override void OnMouseDown(MouseEventArgs e)
        {
            Vector2 mousePos = e.Location.ToVector2();

            if (_debug)
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
            if (!_alive || _transitioning) return;

            switch (e.KeyCode)
            {
                case Keys.P:
                    {
                        // Pause/unpause the game, unless debug mode is on.
                        if (_debug) return;

                        if (IsPaused)
                            Unpause();
                        else
                            Pause();
                    }
                    break;
                case Keys.D:
                    {
                        // Enable/disable debug mode when Ctrl+D is pressed, unless paused or frozen.
                        if (!e.Control || IsPaused || _frozen) return;

                        _debug = !_debug;
                        if (_debug) ReleaseMouse();
                        else CaptureMouse();
                    }
                    break;
                case Keys.N:
                    {
                        // Execute a ball update step when N is pressed and debug mode is activated.
                        if (!_debug) return;
                        UpdateBall();
                    }
                    break;
                case Keys.R:
                    {
                        // Reset the ball position when R is pressed and debug mode is activated.
                        if (!_debug) return;
                        _ball.Position = new Vector2(Ui.ClientSize.Width, Ui.ClientSize.Height) / 2;
                    }
                    break;
                case Keys.Q:
                    {
                        // Move back to the main menu when Q is pressed while paused.
                        if (!IsPaused) return;

                        _transitioning = true;
                        _frozen = true;

                        ReleaseMouse();
                        AddFadeOut(() =>
                        {
                            RemoveScreen(this);
                            AddScreen<TitleScreen>();
                        });
                    }
                    break;
            }
        }
        #endregion

        /// <summary>
        /// Hides the mouse and prevents it from escaping the window bounds.
        /// </summary>
        private void CaptureMouse()
        {
            if (_isMouseCaptured) return;
            _isMouseCaptured = true;

            // Move the mouse back to the paddle position to prevent it from teleporting when unpausing.
            Cursor.Position = Ui.PointToScreen(new Point((int)_paddle.Position.X, Ui.ClientSize.Height / 2));

            Cursor.Clip = Ui.RectangleToScreen(Ui.ClientRectangle);
            Cursor.Hide();
        }

        /// <summary>
        /// Shows the mouse and allows it to move beyond the window bounds.
        /// </summary>
        private void ReleaseMouse()
        {
            if (!_isMouseCaptured) return;
            _isMouseCaptured = false;

            Cursor.Clip = Rectangle.Empty;
            Cursor.Show();
        }

        /// <summary>
        /// Pauses the game.
        /// </summary>
        private void Pause()
        {
            if (IsPaused) return;

            ReleaseMouse();
            IsPaused = true;
        }

        /// <summary>
        /// Unpauses the game.
        /// </summary>
        private void Unpause()
        {
            if (!IsPaused) return;

            CaptureMouse();
            IsPaused = false;
        }

        /// <summary>
        /// Ends the game.
        /// </summary>
        private void EndGame()
        {
            if (!_alive) return;
            _alive = false;

            // Release the mouse capture.
            ReleaseMouse();

            // Add the game over screen providing the score and whether the game was won (there are no more bricks).
            AddScreen(new GameOverScreen(Manager, _scoreOverlay.Value, _bricks.Count == 0));
        }

        private void UpdateBall()
        {
            // Move the ball by its velocity.
            Vector2 v = _ball.Velocity;
            MoveBall(_ball, ref v);

            // End the game if the ball falls below the game area.
            if (_ball.Position.Y > (Ui.ClientSize.Height + 100))
            {
                EndGame();
            }
        }

        /// <summary>
        /// Moves the ball by the specified velocity and handles collisions between bricks.
        /// </summary>
        private void MoveBall(Ball ball, ref Vector2 v)
        {
            // Move the ball by its velocity.
            ball.Move(v);

            Vector2 ballVelocity = ball.Velocity;

            // Detect collisions
            RectangleF ballBounds = ball.Bounds;

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

            // Bounce the ball off the side walls.
            if ((ball.Velocity.X < 0 && ball.Position.X < ball.Radius) ||
                (ball.Velocity.X > 0 && ball.Position.X > (Ui.ClientSize.Width - ball.Radius)))
            {
                ball.Velocity *= new Vector2(-1, 1);
            }

            // Bounce the ball off the ceiling.
            if (ball.Velocity.Y < 0 && ball.Position.Y < ball.Radius)
            {
                ball.Velocity *= new Vector2(1, -1);
            }

            // Calculate collisions with bricks.
            // Define the collision brick, maximum collision area
            // and total intersection bounds.
            float maxCollisionArea = 0;
            Brick collisionBrick = null;
            RectangleF totalIntersection = default;

            for (int i = 0; i < _bricks.Count; i++)
            {
                Brick brick = _bricks[i];

                // Get the brick bounds and calculate the intersection area with the ball bounds.
                RectangleF bounds = brick.Bounds;
                RectangleF intersection = RectangleF.Intersect(ballBounds, bounds);

                // Skip this brick if there is no collision.
                if (intersection.IsEmpty) continue;

                // Take the brick with the largest intersection area in case of collisions with multiple bricks.
                float area = intersection.Width * intersection.Height;
                if (area > maxCollisionArea)
                {
                    maxCollisionArea = area;
                    collisionBrick = brick;
                }

                // Add up the total intersection area in case of collision with multiple bricks.
                if (totalIntersection.IsEmpty)
                {
                    totalIntersection = intersection;
                }
                else
                {
                    totalIntersection = RectangleF.Union(intersection, totalIntersection);
                }

            }

            // If the ball collided with a brick.
            if (collisionBrick != null)
            {
                // Handle the collision between the ball and brick.
                HandleCollision(ball, collisionBrick);

                // Determine if the brick was hit on the sides or the top/bottom
                // by checking if the total intersection area is wider or taller.
                // Then negate the X or Y velocity depending on which side was hit.
                if (totalIntersection.Width > totalIntersection.Height)
                {
                    ball.Velocity *= new Vector2(1, -1);
                }
                else
                {
                    ball.Velocity *= new Vector2(-1, 1);
                }
            }

            // Play a sound when the ball bounces.
            if (ballVelocity != ball.Velocity)
            {
                Sound.Play(_hitSound);
            }
        }

        private void HandleCollision(Ball ball, Brick brick)
        {
            // Get the color of the brick.
            Color color = Theme.FirmnessColors[brick.Firmness];

            if (ball.Color == color)
            {
                // If the ball is the same color as the brick, increment the combo counter.
                _combo++;
            }
            else
            {
                // Otherwise reset the combo counter.
                _combo = 0;

                // Change the ball color to the brick's color and add a visual hit effect.
                ball.Color = color;
                _hitRings.Add(new HitRing(ball.Position, ball.Radius, color));
            }

            // Calculate the score to be added.
            int additionalScore = 10;

            // If the brick's firmness is zero, destroy it and add an extra 5 points.
            if (--brick.Firmness == 0)
            {
                _bricks.Remove(brick);
                additionalScore += 5;

                if (_bricks.Count == 0)
                {
                    // End the game if there are no more bricks.
                    EndGame();
                }
            }

            // Add extra points for the combo counter.
            additionalScore += _combo;
            Score += additionalScore;

            // Display a visual effect indicating the score added where the brick was hit.
            _hitScores.Add(new HitScore(
                Manager,
                brick.Position + brick.Size / 2,
                $"{additionalScore}",
                Theme.FirmnessColors[brick.Firmness + 1]
            ));
        }

        protected override void OnUpdate()
        {
            if (IsPaused) return;

            // Update visual elements (hit scores, hit rings, score overlay).

            for (int i = 0; i < _hitScores.Count; i++)
                if (!_hitScores[i].Update())
                    _hitScores.RemoveAt(i--);

            for (int i = 0; i < _hitRings.Count; i++)
                if (!_hitRings[i].Update())
                    _hitRings.RemoveAt(i--);

            _scoreOverlay.Update();

            // Don't update if we are dead or debugging.
            if (!_alive || _debug) return;

            // Update the ball if it is not frozen.
            if (!_frozen)
            {
                UpdateBall();
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