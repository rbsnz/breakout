using System.Drawing;

using Breakout.Data;

namespace Breakout.Game
{
    public class GameOverScreen : GameScreen
    {
        private readonly Font _font;

        public GameOverScreen(GameManager manager)
            : base(manager)
        {
            _font = Font.GetFont(Theme.FontFamily, 20.0f);
        }

        protected override void OnDraw(Graphics g)
        {
            g.DrawString(
                "GAME OVER!",
                _font,
                Brushes.Magenta,
                new PointF(Ui.ClientSize.Width / 2, Ui.ClientSize.Height / 2),
                StringFormats.Center
            );
        }
    }
}
