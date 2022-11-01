using System.Drawing;
using System.Numerics;

namespace Breakout.Game
{
    public class Paddle : IDrawable
    {
        const float LerpAmount = 1 / 3f;

        private readonly Pen _pen;
        private readonly Brush _brush;

        private Vector2 _position;

        public SizeF Size { get; set; }

        public Vector2 Position
        {
            get => _position;
            set
            {
                _position = value;
                TargetPosition = value;
            }
        }

        public Vector2 TargetPosition { get; set; }

        public RectangleF Bounds => new RectangleF(
            Position.X - Size.Width / 2,
            Position.Y - Size.Height / 2,
            Size.Width, Size.Height
        );

        public Paddle()
        {
            _pen = new Pen(Color.Cyan, 2.0f);
            _brush = new SolidBrush(Color.FromArgb(100, _pen.Color));
        }

        public void Update()
        {
            _position = Vector2.Lerp(_position, TargetPosition, LerpAmount);
        }

        public void Draw(Graphics g)
        {
            RectangleF rect = new RectangleF(Position.ToPointF(), Size);
            rect.X -= Size.Width / 2; rect.Y -= Size.Height / 2;
            g.FillRectangle(_brush, rect);
            g.DrawRectangle(_pen, rect);
        }
    }
}
