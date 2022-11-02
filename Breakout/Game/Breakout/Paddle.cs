using System.Drawing;
using System.Numerics;

namespace Breakout.Game
{
    /// <summary>
    /// Represents a breakout paddle.
    /// </summary>
    public class Paddle : IDrawable
    {
        /// <summary>
        /// The amount to interpolate movement towards the target position.
        /// </summary>
        const float LerpAmount = 1 / 3f;

        private readonly Pen _pen;
        private readonly Brush _brush;

        private Vector2 _position;

        /// <summary>
        /// Gets or sets the size of this paddle.
        /// </summary>
        public SizeF Size { get; set; }

        /// <summary>
        /// Gets or sets the position of this paddle.
        /// </summary>
        public Vector2 Position
        {
            get => _position;
            set
            {
                _position = value;
                TargetPosition = value;
            }
        }

        /// <summary>
        /// Gets or sets the target position of this paddle.
        /// </summary>
        public Vector2 TargetPosition { get; set; }

        /// <summary>
        /// Gets the bounds of this paddle.
        /// </summary>
        public RectangleF Bounds => new RectangleF(
            Position.X - Size.Width / 2,
            Position.Y - Size.Height / 2,
            Size.Width, Size.Height
        );

        /// <summary>
        /// Constructs a new paddle.
        /// </summary>
        public Paddle()
        {
            _pen = new Pen(Color.Cyan, 2.0f);
            _brush = new SolidBrush(Color.FromArgb(100, _pen.Color));
        }

        /// <summary>
        /// Updates this paddle.
        /// </summary>
        public void Update()
        {
            _position = Vector2.Lerp(_position, TargetPosition, LerpAmount);
        }

        /// <summary>
        /// Draws this paddle to the specified render target.
        /// </summary>
        public void Draw(Graphics g)
        {
            RectangleF rect = new RectangleF(Position.ToPointF(), Size);
            rect.X -= Size.Width / 2; rect.Y -= Size.Height / 2;
            g.FillRectangle(_brush, rect);
            g.DrawRectangle(_pen, rect);
        }
    }
}
