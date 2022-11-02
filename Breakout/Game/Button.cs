using System.Drawing;
using System.Numerics;

namespace Breakout.Game
{
    /// <summary>
    /// Represents a button that has a hover state.
    /// </summary>
    public class Button : IDrawable
    {
        private float _transition = 0.0f;

        /// <summary>
        /// Gets the font used by this button.
        /// </summary>
        public Font Font { get; }

        /// <summary>
        /// Gets the text of this button.
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Gets or sets the position of this button.
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// Gets the size of this button.
        /// </summary>
        public SizeF Size { get; }

        /// <summary>
        /// Gets the bounds of this button.
        /// </summary>
        public RectangleF Bounds => new RectangleF(Position.ToPointF(), Size);

        /// <summary>
        /// Gets or sets whether the hover state of this button.
        /// </summary>
        public bool Hover { get; set; }

        /// <summary>
        /// Constructs a new button with the specified font and text.
        /// </summary>
        public Button(GameManager manager, Font font, string text)
        {
            Font = font;
            Text = text;

            Size = manager.Ui.MeasureString(Text, Font);
        }

        /// <summary>
        /// Updates the hover transition of this button.
        /// </summary>
        public void Update()
        {
            float target = Hover ? 1 : 0;
            _transition += (target - _transition) * 0.3f;
        }

        /// <summary>
        /// Draws this button to the specified render target.
        /// </summary>
        public void Draw(Graphics g)
        {
            g.DrawString(Text, Font, Brushes.Magenta, Position.ToPointF());
            g.DrawString(Text, Font, Brushes.Yellow, (Position - new Vector2(Font.Size / 8, Font.Size / 8) * _transition).ToPointF());
        }
    }
}
