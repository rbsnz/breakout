using System.Drawing;

namespace Breakout.Game
{
    /// <summary>
    /// Displays text and dims the screen while the game is paused.
    /// </summary>
    public class PauseScreen : GameScreen
    {
        private readonly Font _font;
        private readonly Dimmer _dimmer;

        private bool _isPaused;
        /// <summary>
        /// Gets or sets whether the game is paused.
        /// </summary>
        public bool IsPaused
        {
            get => _isPaused;
            set
            {
                _isPaused = value;
                _dimmer.Dim = value;
                if (!value) _dimmer.Opacity = 0;
            }
        }

        /// <summary>
        /// Constructs a new pause screen.
        /// </summary>
        public PauseScreen(GameManager manager)
            : base(manager)
        {
            _font = Font.GetFont(Theme.FontFamily, 20.0f);
            _dimmer = new Dimmer() { Dim = false };
        }

        protected override void OnUpdate()
        {
            // Update the dimmer.
            _dimmer.Update();
        }

        protected override void OnDraw(Graphics g)
        {
            // Draw the dimmer and pause text while the game is paused.
            if (IsPaused)
            {
                PointF textPos = new PointF(Ui.ClientSize.Width / 2, Ui.ClientSize.Height / 2);

                _dimmer.Draw(g);
                g.DrawStringAligned("PAUSED\n\nPress Q to quit", _font, Brushes.Cyan, textPos, ContentAlignment.MiddleCenter);
            }
        }
    }
}
