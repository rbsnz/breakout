using System.Drawing;
using System.Numerics;

namespace Breakout.Game
{
    /// <summary>
    /// Represents a visual text element.
    /// </summary>
    public class Text : IDrawable
    {
        private readonly GameManager _manager;

        private Color _color = Color.White;
        private Brush _brush;
        private string _value;

        /// <summary>
        /// Gets the font of this text.
        /// </summary>
        public Font Font { get; }

        /// <summary>
        /// Gets or sets the position of this text.
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// Gets or sets the value of this text.
        /// </summary>
        public string Value
        {
            get => _value;
            set
            {
                _value = value;
                UpdateSize();
            }
        }

        /// <summary>
        /// Gets or sets the color of this text.
        /// </summary>
        public Color Color
        {
            get => _color;
            set
            {
                _color = value;
                UpdateBrush();
            }
        }

        /// <summary>
        /// Gets or sets the alignment of this text.
        /// </summary>
        public ContentAlignment Alignment { get; set; }

        /// <summary>
        /// Gets the measured size of this text.
        /// </summary>
        public SizeF Size { get; private set; }

        /// <summary>
        /// Constructs a new text with the specified font.
        /// </summary>
        public Text(GameManager manager, Font font)
        {
            _manager = manager;
            Font = font;
            Alignment = ContentAlignment.MiddleCenter;

            UpdateSize();
            UpdateBrush();
        }

        private void UpdateSize() => Size = _manager.Ui.MeasureString(Value, Font);

        private void UpdateBrush() => _brush = new SolidBrush(Color);

        public void Draw(Graphics g) => g.DrawStringAligned(_value, Font, _brush, Position.ToPointF(), Alignment);
    }
}
