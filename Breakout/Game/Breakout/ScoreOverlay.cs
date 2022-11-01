using System.Drawing;
using System.Numerics;

using Breakout.Fonts;

namespace Breakout.Game
{
    public class ScoreOverlay : IDrawable
    {
        private readonly IUiManager _uiManager;

        private readonly Font _font;

        private SizeF _size;

        private int _value;
        public int Value
        {
            get => _value;
            set
            {
                _value = value;
                _size = _uiManager.MeasureString(Text, _font);
            }
        }

        public Vector2 Position { get; set; }
        public StringFormat Format { get; set; }

        public string Text => $"Score: {_value}";

        private readonly Paddle _paddle;

        public ScoreOverlay(FontManager fontManager, IUiManager uiManager, Paddle paddle)
        {
            _uiManager = uiManager;

            _font = fontManager.GetFont(Theme.FontFamily, 16.0f);

            _paddle = paddle;

            Position = Vector2.Zero;
            Format = StringFormat.GenericDefault;

            Value = 0;

            PositionLeft();
        }

        private void PositionLeft()
        {
            Position = new Vector2(10, _uiManager.ClientSize.Height - 10);
            Format = StringFormats.BottomLeft;
        }

        private void PositionRight()
        {
            Position = new Vector2(_uiManager.ClientSize.Width - 10, _uiManager.ClientSize.Height - 10);
            Format = StringFormats.BottomRight;
        }

        public void Update()
        {
            float
                centerX = _uiManager.ClientSize.Width / 2,
                paddleLeft = _paddle.Position.X - (_paddle.Size.Width / 2),
                paddleRight = _paddle.Position.X + (_paddle.Size.Width / 2);

            if (Position.X < centerX && paddleLeft < (Position.X + _size.Width + 10))
            {
                PositionRight();
            }
            else if (Position.X > centerX && paddleRight > (Position.X - _size.Width - 10))
            {
                PositionLeft();
            }
        }

        public void Draw(Graphics g)
        {
            g.DrawString(Text, _font, Brushes.Yellow, Position.ToPointF(), Format);
        }
    }
}
