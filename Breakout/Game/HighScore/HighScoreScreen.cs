
using Breakout.Data;
using System.Collections.Generic;
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

        private readonly List<HighScore> _highScores;

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

            _highScores = manager.Scores.Load();


        }

        protected override void OnAdd() => AddFadeIn();

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
                AddFadeOut(() =>
                {
                    RemoveScreen(this);
                    AddScreen<TitleScreen>();
                });
            }
        }

        protected override void OnUpdate()
        {
            if (_transitioning) return;

            _backButton.Update();
        }

        protected override void OnDraw(Graphics g)
        {
            g.DrawStringAligned("HIGH SCORES", _titleFont, Brushes.Yellow, new PointF(Ui.ClientSize.Width / 2, 20), ContentAlignment.TopCenter);

            _backButton.Draw(g);
        }
    }
}
