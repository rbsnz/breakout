
using Breakout.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;

namespace Breakout.Game
{
    public class HighScoreScreen : GameScreen
    {
        private readonly Font _titleFont;

        private readonly Font _textFont;

        private readonly Button _backButton;

        private bool _transitioning;

        private readonly Text _scoreText;

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

            try
            {
                if (HighScores.Count == 0)
                {
                    _scoreText = new Text(Manager, _textFont)
                    {
                        Value = "No high scores yet.\n\nBe the first!",
                        Color = Color.Magenta,
                        Position = Manager.Ui.ClientSize.ToVector2() / 2
                    };
                }
                else
                {
                    string scoreText = "";
                    int maxNameLength = HighScores.Max(x => x.Name.Length);
                    for (int i = 0; i < HighScores.Count; i++)
                    {
                        if (i > 0)
                            scoreText += "\n\n";
                        var highScore = HighScores[i];
                        scoreText += $"{highScore.Name.PadLeft(maxNameLength)}: {highScore.Score}";
                    }

                    _scoreText = new Text(Manager, _textFont)
                    {
                        Value = scoreText,
                        Color = Color.Cyan,
                        Position = Manager.Ui.ClientSize.ToVector2() / 2
                    };
                }
            }
            catch
            {
                _scoreText = new Text(Manager, _textFont)
                {
                    Value = "Failed to load scores :(\n\nThe file may be corrupt.",
                    Color = Color.Red,
                    Position = Manager.Ui.ClientSize.ToVector2() / 2
                };

                Manager.HighScores.Reset();
            }
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

            _scoreText.Draw(g);

            _backButton.Draw(g);
        }
    }
}
