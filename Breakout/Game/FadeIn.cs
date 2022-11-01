using System;
using System.Drawing;

namespace Breakout.Game
{
    public class FadeIn : GameScreen
    {
        private Brush _brush = Brushes.Black;
        private float _opacity = 1.0f;

        private DateTime _created;

        public Action OnComplete { get; set; }

        public FadeIn(GameManager manager)
            : base(manager)
        {
            _created = DateTime.Now;
        }

        protected override void OnUpdate()
        {
            if ((DateTime.Now - _created).TotalSeconds < 0.1) return;

            _opacity -= 0.03f;
            _brush = new SolidBrush(Color.FromArgb((int)(Math.Max(0, _opacity) * 255), Color.Black));

            if (_opacity <= 0)
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
