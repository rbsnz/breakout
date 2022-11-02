using System.Drawing;

namespace Breakout.Game
{
    public class Dimmer : IDrawable
    {
        private float _opacity;
        private Brush _backgroundBrush;

        public bool Dim { get; set; } = true;
        public float Strength { get; set; } = 0.5f;
        public float Opacity
        {
            get => _opacity;
            set
            {
                value = Maths.Clamp(value, 0, 1);
                if (_opacity == value) return;

                _opacity = value;
                _backgroundBrush = new SolidBrush(Color.FromArgb((int)(_opacity * 255), Color.Black));
            }
        }
        public float LerpAmount { get; set; } = 0.03f;

        public Dimmer() => _backgroundBrush = Brushes.Transparent;

        public void Update() => Opacity += ((Dim ? Strength : 0) - _opacity) * LerpAmount;

        public void Draw(Graphics g) => g.FillRectangle(_backgroundBrush, g.ClipBounds);
    }
}
