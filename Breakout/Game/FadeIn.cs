using System;
using System.Drawing;

namespace Breakout.Game
{
    public class FadeIn : GameScreen
    {
        private readonly DateTime _created;
        private readonly Dimmer _dimmer;

        public Action OnComplete { get; set; }

        public FadeIn(GameManager manager)
            : base(manager)
        {
            _created = DateTime.Now;
            _dimmer = new Dimmer
            {
                Dim = false,
                Opacity = 1,
                Strength = 1.0f,
                LerpAmount = 0.1f
            };
        }

        protected override void OnUpdate()
        {
            if ((DateTime.Now - _created).TotalSeconds < 0.2) return;

            _dimmer.Update();

            if (_dimmer.Opacity <= 0.05f)
            {
                Manager.RemoveScreen(this);
                OnComplete?.Invoke();
            }
        }

        protected override void OnDraw(Graphics g) => _dimmer.Draw(g);
    }
}
