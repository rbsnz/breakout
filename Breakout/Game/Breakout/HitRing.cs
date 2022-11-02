using System.Drawing;
using System.Numerics;

namespace Breakout.Game
{
    public class HitRing : IDrawable
    {
        private float _opacity;
        private Pen _pen;
        private Brush _brush;

        public Color Color { get; }
        public Vector2 Position { get; set; }
        public float Radius { get; set; }
        public float Scale { get; set; } = 1.0f;
        public float Alpha
        {
            get => _opacity;
            set
            {
                _opacity = value;
                _pen = new Pen(Color.WithAlpha(Alpha));
                _brush = new SolidBrush(Color.WithAlpha(Alpha));
            }
        }

        public HitRing(Vector2 position, float radius, Color color)
        {
            Position = position;
            Radius = radius;
            Color = color;
            Alpha = 1.0f;
        }

        public bool Update()
        {
            Scale *= 1.05f;
            Scale *= 1.05f;
            Alpha = 1.0f - Scale / 8;
            return Alpha > 0;
        }

        public void Draw(Graphics g)
        {
            g.FillCircle(_brush, Position, Radius * Scale);
        }
    }
}
