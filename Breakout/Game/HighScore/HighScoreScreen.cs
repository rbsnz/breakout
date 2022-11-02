using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;

namespace Breakout.Game
{
    /// <summary>
    /// Represents a screen where high scores can be viewed.
    /// </summary>
    public class HighScoreScreen : GameScreen
    {
        private readonly Font _titleFont;
        private readonly Font _textFont;

        private readonly Button _backButton;

        private readonly Text _titleText;
        private readonly List<Text> _scoreTexts = new List<Text>();

        private bool _transitioning;

        /// <summary>
        /// Constructs a new high score screen.
        /// </summary>
        public HighScoreScreen(GameManager manager)
            : base(manager)
        {
            // Calculate the positioning of texts.
            _titleFont = Font.GetFont(Theme.FontFamily, 20.0f);
            _textFont = Font.GetFont(Theme.FontFamily, 16.0f);

            _titleText = new Text(Manager, _titleFont)
            {
                Value = $"TOP {HighScores.MaxScores}\nHIGH SCORES",
                Color = Color.Yellow,
                Position = new Vector2(Ui.ClientSize.Width / 2, 20),
                Alignment = ContentAlignment.TopCenter
            };

            _backButton = new Button(Manager, _textFont, "Back");
            _backButton.Position = new Vector2(
                Ui.ClientSize.Width / 2 - _backButton.Size.Width / 2,
                Ui.ClientSize.Height - 20 - _backButton.Size.Height
            );

            if (HighScores.Count == 0)
            {
                _scoreTexts.Add(new Text(Manager, _textFont)
                {
                    Value = "No high scores yet.\n\nBe the first!",
                    Color = Color.Magenta,
                    Position = Manager.Ui.ClientSize.ToVector2() / 2
                });
            }
            else
            {
                var nameTexts = new List<Text>(
                    HighScores.Select(x => new Text(Manager, _textFont)
                    {
                        Value = $"{x.Name}:",
                        Color = Color.Cyan,
                        Alignment = ContentAlignment.TopRight,
                    })
                );

                var scoreTexts = new List<Text>(
                    HighScores.Select(x => new Text(Manager, _textFont)
                    {
                        Value = $"{x.Score}",
                        Color = Color.Cyan,
                        Alignment = ContentAlignment.TopLeft
                    })
                );

                // Calculate the total height of all score texts + spacing.
                float spacing = 40.0f;
                float totalHeight = nameTexts.Sum(x => x.Size.Height) + (nameTexts.Count - 1) * spacing;

                // Calculate the positions of score texts.
                Vector2 position = new Vector2(Manager.Ui.ClientSize.Width, Manager.Ui.ClientSize.Height - totalHeight) / 2;
                for (int i = 0; i < nameTexts.Count; i++)
                {
                    nameTexts[i].Position = position - new Vector2(5, 0);
                    scoreTexts[i].Position = position + new Vector2(5, 0);
                    position.Y += nameTexts[i].Size.Height + spacing;
                }

                _scoreTexts.AddRange(nameTexts.Concat(scoreTexts));
            }
        }

        protected override void OnAdd() => AddFadeIn();

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_transitioning) return;

            _backButton.Hover = _backButton.Bounds.Contains(e.Location);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (_transitioning) return;

            // Move back to the title screen when the back button is clicked.
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
            _titleText.Draw(g);
            foreach (var text in _scoreTexts)
                text.Draw(g);
            _backButton.Draw(g);
        }
    }
}
