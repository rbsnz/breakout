using System.Drawing;
using System.Numerics;

namespace Breakout.Game
{
    /// <summary>
    /// Displays the current score to the screen, avoiding drawing text over the paddle.
    /// </summary>
    public class ScoreOverlay : IDrawable
    {
        private readonly GameManager _manager;
        private readonly Font _font;

        private readonly Paddle _paddle;
        private readonly Text _text;

        private int _value;

        /// <summary>
        /// Gets or sets the score value.
        /// </summary>
        public int Value
        {
            get => _value;
            set
            {
                _value = value;
                _text.Value = $"Score: {value}";
            }
        }

        /// <summary>
        /// Constructs a new score overlay.
        /// </summary>
        public ScoreOverlay(GameManager manager, Paddle paddle)
        {
            _manager = manager;
            _paddle = paddle;

            _font = _manager.Font.GetFont(Theme.FontFamily, 16.0f);

            _text = new Text(_manager, _font) { Color = Color.Yellow };

            Value = 0;
            PositionLeft();
        }

        /// <summary>
        /// Positions the text to the left side of the screen.
        /// </summary>
        private void PositionLeft()
        {
            _text.Position = new Vector2(10, _manager.Ui.ClientSize.Height - 10);
            _text.Alignment = ContentAlignment.BottomLeft;
        }

        /// <summary>
        /// Positions the text to the right side of the screen.
        /// </summary>
        private void PositionRight()
        {
            _text.Position = new Vector2(_manager.Ui.ClientSize.Width - 10, _manager.Ui.ClientSize.Height - 10);
            _text.Alignment = ContentAlignment.BottomRight;
        }

        /// <summary>
        /// Updates the position of the text depending on the paddle position,
        /// ensuring that the score text does not display over the paddle.
        /// </summary>
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

        /// <summary>
        /// Draws the score to the screen.
        /// </summary>
        public void Draw(Graphics g) => _text.Draw(g);
    }
}
