using System.Drawing;
using System.Numerics;

namespace Breakout.Game
{
    /// <summary>
    /// Represents a breakout ball.
    /// </summary>
    public class Ball : IDrawable
    {
        private Color _color;
        private Pen _pen;
        private Brush _brush;

        /// <summary>
        /// Gets or sets the position of this ball.
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// Gets or sets the velocity of this ball.
        /// </summary>
        public Vector2 Velocity { get; set; }

        /// <summary>
        /// Gets or sets the radius of this ball.
        /// </summary>
        public float Radius { get; set; } = 8.0f;

        /// <summary>
        /// Gets the bounding rectangle of this ball.
        /// </summary>
        public RectangleF Bounds => new RectangleF(Position.X - Radius, Position.Y - Radius, Radius * 2, Radius * 2);


        /// <summary>
        /// Gets or sets the color of the ball.
        /// </summary>
        public Color Color
        {
            get => _color;
            set
            {
                _color = value;
                _pen = new Pen(_color, 2.0f);
                _brush = new SolidBrush(Color.FromArgb(100, _color));
            }
        }

        /// <summary>
        /// Constructs a new ball.
        /// </summary>
        public Ball()
            : this(Vector2.Zero)
        { }

        /// <summary>
        /// Constructs a new ball at the specified position.
        /// </summary>
        public Ball(Vector2 position)
        {
            Position = position;
            Velocity = Vector2.Zero;
        }

        /// <summary>
        /// Moves the ball by the specified offset relative to its current location.
        /// </summary>
        public void Move(Vector2 offset)
        {
            Position += offset;
        }

        /// <summary>
        /// Draws the ball to the specified render target.
        /// </summary>
        public void Draw(Graphics g)
        {
            g.FillCircle(_brush, Position, Radius);
            g.DrawCircle(_pen, Position, Radius);
        }
    }
}
