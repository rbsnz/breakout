
using System.Drawing;
using System.Numerics;

namespace Breakout.Game
{
    public class HighScoreScreen : GameScreen
    {
        private readonly Font _titleFont;

        private readonly Font _textFont;

        private readonly Button _backButton;

        private bool _transitioning;

        public HighScoreScreen(GameManager manager)
            : base(manager)
        {
            _titleFont = Font.GetFont(Theme.FontFamily, 20.0f);

            _textFont = Font.GetFont(Theme.FontFamily, 16.0f);

            _backButton = new Button(Manager, _textFont, "Back");
            _backButton.Position = new Vector2(
                Ui.ClientSize.Width / 2 - _backButton.Size.Width / 2,
                Ui.ClientSize.Height - 20 - _backButton.Size.Height
            );
        }

        protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
        {
            if (_transitioning) return;

            _backButton.Hover = _backButton.Bounds.Contains(e.Location);
        }

        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            if (_transitioning) return;

            if (_backButton.Bounds.Contains(e.Location))
            {
                _transitioning = true;
                Manager.AddScreen(new FadeOut(Manager, () =>
                {
                    Manager.RemoveScreen(this);
                    Manager.AddScreen(new TitleScreen(Manager));
                    Manager.AddScreen(new FadeIn(Manager));
                }));
            }
        }

        protected override void OnUpdate()
        {
            if (_transitioning) return;

            _backButton.Update();
        }

        protected override void OnDraw(Graphics g)
        {
            g.DrawString("HIGH SCORES", _titleFont, Brushes.Yellow, new PointF(Ui.ClientSize.Width / 2, 20), StringFormats.TopCenter);

            _backButton.Draw(g);
        }
    }
}
