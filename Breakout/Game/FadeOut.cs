using System;
using System.Drawing;

using Breakout.Data;

namespace Breakout.Game
{
    public class FadeOut : GameScreen
    {
        private readonly Action _onFadeOut;

        private float _opacity = 0.0f;
        private Brush _brush;

        public FadeOut(GameManager manager, Action onFadeOut = null)
            : base(manager)
        {
            _onFadeOut = onFadeOut;
            _brush = Brushes.Transparent;
        }

        protected override void OnUpdate()
        {
            _opacity += (1 - _opacity) * 0.15f;
            if (_opacity > 1)
                _opacity = 1;

            _brush = new SolidBrush(Color.FromArgb((int)(255 * _opacity), Color.Black));

            if (_opacity >= 0.99f)
            {
                Manager.RemoveScreen(this);
                _onFadeOut?.Invoke();
            }
        }

        protected override void OnDraw(Graphics g)
        {
            g.FillRectangle(_brush, Ui.ClientRectangle);
        }
    }
}
