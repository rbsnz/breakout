using System;
using System.Drawing;
using System.Numerics;
using Breakout.Fonts;
using Breakout.Data;

namespace Breakout.Game
{
    /// <summary>
    /// A text that floats indicating the score obtained when a brick is hit.
    /// </summary>
    public class HitScore : IGameObject
    {
        private readonly Font _font;
        private Brush _brush;
        private float _alpha;

        public Vector2 Position { get; }
        public RectangleF Bounds => RectangleF.Empty;
        public string Text { get; }
        public float Scale { get; set; }
        public float Alpha
        {
            get => _alpha;
            set
            {
                _alpha = (float)Math.Max(0, Math.Min(1, value));
                _brush = new SolidBrush(Color.FromArgb((int)(255 * Alpha), Color));
            }
        }
        public Color Color { get; }

        public bool IsDead => throw new NotImplementedException();

        public HitScore(FontManager fontManager, Vector2 position, string text, Color color)
        {
            _font = fontManager.GetFont(Theme.FontFamily, 20.0f);

            Position = position;
            Text = text;
            Color = color;
            Scale = 1.0f;
            Alpha = 1.0f;
        }

        public bool Update()
        {
            Scale *= 1.05f;
            Alpha = 1.0f - (Scale / 3);
            return Alpha > 0;
        }

        public void Draw(Graphics g)
        {
            g.TranslateTransform(Position.X, Position.Y);
            g.ScaleTransform(Scale, Scale);
            g.DrawString(Text, _font, _brush, PointF.Empty, StringFormats.Center);
            g.ResetTransform();
        }
    }
}
