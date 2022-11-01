using System.Drawing;
using System.Numerics;

using Breakout.Fonts;

namespace Breakout.Game
{
    public class ScoreOverlay : IDrawable
    {
        private readonly IUiManager _uiManager;

        private readonly Font _font;

        public int Value { get; set; }

        public ScoreOverlay(FontManager fontManager, IUiManager uiManager)
        {
            _uiManager = uiManager;

            _font = fontManager.GetFont(Theme.FontFamily, 16.0f);
        }

        public void Draw(Graphics g)
        {
            g.DrawString(
                $"Score: {Value}",
                _font, Brushes.Yellow,
                new PointF(10, _uiManager.ClientSize.Height - 10),
                StringFormats.BottomLeft
            );
        }
    }
}
