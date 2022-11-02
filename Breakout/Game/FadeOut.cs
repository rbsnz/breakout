using System;
using System.Drawing;

namespace Breakout.Game
{
    public class FadeOut : GameScreen
    {
        private readonly Dimmer _dimmer;

        public Action OnComplete { get; set; }

        public FadeOut(GameManager manager)
            : base(manager)
        {
            _dimmer = new Dimmer
            {
                Dim = true,
                Opacity = 0.0f,
                Strength = 1.0f,
                LerpAmount = 0.1f
            };
        }

        protected override void OnUpdate()
        {
            _dimmer.Update();

            if (_dimmer.Opacity >= 0.95f)
            {
                Manager.RemoveScreen(this);
                OnComplete?.Invoke();
            }
        }

        protected override void OnDraw(Graphics g) => _dimmer.Draw(g);
    }
}
