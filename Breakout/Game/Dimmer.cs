using System.Drawing;

namespace Breakout.Game
{
    /// <summary>
    /// An overlay that smoothly fades in and out, used to dim the screen.
    /// </summary>
    public class Dimmer : IDrawable
    {
        private float _opacity;
        private Brush _backgroundBrush;

        /// <summary>
        /// Gets or sets whether to dim the screen.
        /// </summary>
        public bool Dim { get; set; } = true;

        /// <summary>
        /// Gets or sets the strength of the dimming effect.
        /// </summary>
        public float Strength { get; set; } = 0.5f;

        /// <summary>
        /// Gets or sets the opacity of this dimmer.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the linear interpolation amount used to animate this dimmer.
        /// </summary>
        public float LerpAmount { get; set; } = 0.03f;

        /// <summary>
        /// Constructs a new dimmer.
        /// </summary>
        public Dimmer() => _backgroundBrush = Brushes.Transparent;

        /// <summary>
        /// Updates the opacity of this dimmer.
        /// </summary>
        public void Update() => Opacity += ((Dim ? Strength : 0) - _opacity) * LerpAmount;

        /// <summary>
        /// Draws this dimmer to the specified render target.
        /// </summary>
        public void Draw(Graphics g) => g.FillRectangle(_backgroundBrush, g.ClipBounds);
    }
}
