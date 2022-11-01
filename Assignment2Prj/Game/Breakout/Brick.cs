using System.Drawing;
using System.Numerics;

namespace Breakout.Game
{
    public class Brick : IDrawable
    {
        private Vector2 _position;
        private Vector2 _size;
        private RectangleF _visualRectangle;

        public Vector2 Position
        {
            get => _position;
            set
            {
                _position = value;
                UpdateGeometry();
            }
        }

        public Vector2 Size
        {
            get => _size;
            set
            {
                _size = value;
                UpdateGeometry();
            }
        }

        public RectangleF Bounds => new RectangleF(Position.X, Position.Y, Size.X, Size.Y);

        public int Firmness { get; set; } = 4;

        public float Top => Position.Y;
        public float Left => Position.X;
        public float Bottom => Position.Y + Size.Y;
        public float Right => Position.X + Size.X;
        public Vector2 TopLeft => new Vector2(Left, Top);
        public Vector2 TopRight => new Vector2(Right, Top);
        public Vector2 BottomLeft => new Vector2(Left, Bottom);
        public Vector2 BottomRight => new Vector2(Right, Bottom);

        public Brick(Vector2 position, Vector2 size)
        {
            Position = position;
            Size = size;
        }

        private void UpdateGeometry()
        {
            _visualRectangle = RectangleF.Inflate(Bounds, -2, -2);
        }

        public void Draw(Graphics g)
        {
            g.FillRectangle(Theme.FirmnessBrushes[Firmness], _visualRectangle);
            g.DrawRectangle(Theme.FirmnessPens[Firmness], _visualRectangle);
        }
    }
}
