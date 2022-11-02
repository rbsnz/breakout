using System.Drawing;

namespace Breakout.Game
{
    public class PauseScreen : GameScreen
    {
        private readonly Font _font;

        private readonly Dimmer _dimmer;

        private bool _isPaused;
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

        public PauseScreen(GameManager manager)
            : base(manager)
        {
            _font = Font.GetFont(Theme.FontFamily, 20.0f);
            _dimmer = new Dimmer() { Dim = false };
        }

        protected override void OnUpdate()
        {
            _dimmer.Update();
        }

        protected override void OnDraw(Graphics g)
        {
            if (IsPaused)
            {
                PointF textPos = new PointF(Ui.ClientSize.Width / 2, Ui.ClientSize.Height / 2);

                _dimmer.Draw(g);
                g.DrawStringAligned("PAUSED\n\nPress Q to quit", _font, Brushes.Cyan, textPos, ContentAlignment.MiddleCenter);
            }
        }
    }
}
