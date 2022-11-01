using System;
using System.Drawing;

using Breakout.Data;

namespace Breakout.Game
{
    public class FadeOut : GameScreen
    {
        private float _opacity = 0.0f;
        private Brush _brush;

        public Action OnComplete { get; set; }

        public FadeOut(GameManager manager)
            : base(manager)
        {
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
                OnComplete?.Invoke();
            }
        }

        protected override void OnDraw(Graphics g)
        {
            g.FillRectangle(_brush, Ui.ClientRectangle);
        }
    }
}
