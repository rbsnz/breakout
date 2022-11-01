using System.Drawing;
using System.Numerics;

namespace Breakout.Game
{
    public class Ball : IDrawable
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public float Radius { get; set; } = 8.0f;
        public RectangleF Rect => new RectangleF(Position.X - Radius, Position.Y - Radius, Radius * 2, Radius * 2);

        private Color _color;
        private Pen _pen;
        private Brush _brush;

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

        public Ball()
            : this(Vector2.Zero)
        { }

        public Ball(Vector2 position)
        {
            Position = position;
            Velocity = Vector2.Zero;
        }

        public void Move(Vector2 offset)
        {
            Position += offset;
        }

        public void Bounce(Vector2 normal)
        {
            Velocity = Vector2.Reflect(Velocity, normal);
        }

        public void Draw(Graphics g)
        {
            g.FillCircle(_brush, Position, Radius);
            g.DrawCircle(_pen, Position, Radius);
        }
    }
}
