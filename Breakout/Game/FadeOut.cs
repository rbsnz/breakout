using System;
using System.Drawing;

namespace Breakout.Game
{
    /// <summary>
    /// Represents a fade out effect that may invoke an action once completed.
    /// </summary>
    public class FadeOut : GameScreen
    {
        private readonly Dimmer _dimmer;

        /// <summary>
        /// Gets or sets the action to invoke upon fade completion.
        /// </summary>
        public Action OnComplete { get; set; }

        /// <summary>
        /// Constructs a new fade in effect.
        /// </summary>
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
