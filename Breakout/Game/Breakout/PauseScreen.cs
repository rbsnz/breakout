using Breakout.Fonts;
using System;
using System.Drawing;

namespace Breakout.Game
{
    public class PauseScreen : GameScreen
    {
        private readonly Font _font;

        private Brush _backgroundBrush;
        private float _backgroundOpacity = 0.0f;

        private bool _confirmQuit = false;

        private bool _isPaused;
        public bool IsPaused
        {
            get => _isPaused;
            set
            {
                _isPaused = value;
                if (!_isPaused) Opacity = 0;
            }
        }

        public event EventHandler Quit;

        private float Opacity
        {
            get => _backgroundOpacity;
            set
            {
                value = Math.Max(0, Math.Min(1, value));
                if (value == _backgroundOpacity) return;

                _backgroundOpacity = value;
                _backgroundBrush = new SolidBrush(Color.FromArgb((int)(_backgroundOpacity * 255), Color.Black));
            }
        }

        public PauseScreen(GameManager manager)
            : base(manager)
        {
            _font = Font.GetFont(Theme.FontFamily, 20.0f);
            _backgroundBrush = Brushes.Transparent;
        }

        protected override void OnUpdate()
        {
            if (IsPaused)
            {
                Opacity += (0.5f - Opacity) * 0.05f;
            }
        }

        protected override void OnDraw(Graphics g)
        {
            if (IsPaused)
            {
                PointF textPos = new PointF(Ui.ClientSize.Width / 2, Ui.ClientSize.Height / 2);

                g.FillRectangle(_backgroundBrush, Ui.ClientRectangle);
                g.DrawString("PAUSED\n\nPress Q to quit", _font, Brushes.Cyan, textPos, StringFormats.Center);
            }
        }
    }
}
