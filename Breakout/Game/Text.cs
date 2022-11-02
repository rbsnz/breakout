using System.Drawing;
using System.Numerics;

namespace Breakout.Game
{
    public class Text : IDrawable
    {
        private readonly GameManager _manager;

        private Color _color = Color.White;
        private Brush _brush;
        private string _value;

        public Vector2 Position { get; set; }

        public Font Font { get; }

        public string Value
        {
            get => _value;
            set
            {
                _value = value;
                UpdateSize();
            }
        }

        public ContentAlignment Alignment { get; set; }

        public Color Color
        {
            get => _color;
            set
            {
                _color = value;
                UpdateBrush();
            }
        }

        public SizeF Size { get; private set; }

        public Text(GameManager manager, Font font)
        {
            _manager = manager;
            Font = font;
            Alignment = ContentAlignment.MiddleCenter;

            UpdateSize();
            UpdateBrush();
        }

        private void UpdateSize()
        {
            Size = _manager.Ui.MeasureString(Value, Font);
        }

        private void UpdateBrush()
        {
            _brush = new SolidBrush(Color);
        }

        public void Draw(Graphics g)
        {
            g.DrawStringAligned(_value, Font, _brush, Position.ToPointF(), Alignment);
        }
    }
}
