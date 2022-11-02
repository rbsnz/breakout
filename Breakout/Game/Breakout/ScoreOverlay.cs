using System.Drawing;
using System.Numerics;

using Breakout.Fonts;

namespace Breakout.Game
{
    public class ScoreOverlay : IDrawable
    {
        private readonly GameManager _manager;
        private readonly Font _font;

        private readonly Paddle _paddle;
        private readonly Text _text;

        private int _value;
        public int Value
        {
            get => _value;
            set
            {
                _value = value;
                _text.Value = $"Score: {value}";
            }
        }

        public ScoreOverlay(GameManager manager, Paddle paddle)
        {
            _manager = manager;
            _paddle = paddle;

            _font = _manager.Font.GetFont(Theme.FontFamily, 16.0f);

            _text = new Text(_manager, _font) { Color = Color.Yellow };

            Value = 0;
            PositionLeft();
        }

        private void PositionLeft()
        {
            _text.Position = new Vector2(10, _manager.Ui.ClientSize.Height - 10);
            _text.Alignment = ContentAlignment.BottomLeft;
        }

        private void PositionRight()
        {
            _text.Position = new Vector2(_manager.Ui.ClientSize.Width - 10, _manager.Ui.ClientSize.Height - 10);
            _text.Alignment = ContentAlignment.BottomRight;
        }

        public void Update()
        {
            float
                centerX = _manager.Ui.ClientSize.Width / 2,
                paddleLeft = _paddle.Position.X - (_paddle.Size.Width / 2),
                paddleRight = _paddle.Position.X + (_paddle.Size.Width / 2);

            if (_text.Position.X < centerX && paddleLeft < (_text.Position.X + _text.Size.Width + 10))
            {
                PositionRight();
            }
            else if (_text.Position.X > centerX && paddleRight > (_text.Position.X - _text.Size.Width - 10))
            {
                PositionLeft();
            }
        }

        public void Draw(Graphics g) => _text.Draw(g);
    }
}
