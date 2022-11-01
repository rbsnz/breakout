
using System.Drawing;

namespace Breakout.Game
{
    public class HighScoreScreen : GameScreen
    {
        private readonly Font _titleFont;

        public HighScoreScreen(GameManager manager)
            : base(manager)
        {
            _titleFont = Font.GetFont(Theme.FontFamily, 20.0f);
        }

        protected override void OnUpdate()
        {
            
        }

        protected override void OnDraw(Graphics g)
        {
            g.DrawString("HIGH SCORES", _titleFont, Brushes.Yellow, new PointF(Ui.ClientSize.Width / 2, 20), StringFormats.TopCenter);
        }
    }
}
