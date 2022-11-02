
using System.Drawing;
using System.Numerics;

namespace Breakout.Game
{
    /// <summary>
    /// The title screen that allows the user to transition to the game screen, high score screen, or exit the game.
    /// </summary>
    public class TitleScreen : GameScreen
    {
        private readonly Font _titleFont;
        private readonly Button _buttonPlay, _buttonHighScores, _buttonQuit;
        private readonly Button[] _buttons;

        private readonly Text _titleText, _titleTextShadow;

        private bool _transitioning;

        public TitleScreen(GameManager manager)
            : base(manager)
        {
            _titleFont = Font.GetFont(Theme.FontFamily, 36.0f);

            _titleText = new Text(Manager, _titleFont)
            {
                Value = "BREAKOUT",
                Color = Color.Yellow,
                Alignment = ContentAlignment.TopCenter
            };

            _titleTextShadow = new Text(Manager, _titleFont)
            {
                Value = "BREAKOUT",
                Color = Color.Magenta,
                Alignment = ContentAlignment.TopCenter
            };

            _buttonPlay = new Button(Manager, _titleFont, "Play");
            _buttonHighScores = new Button(Manager, _titleFont, "High Scores");
            _buttonQuit = new Button(Manager, _titleFont, "Quit");

            _buttons = new[] { _buttonPlay, _buttonHighScores, _buttonQuit };
        }

        protected override void OnAdd()
        {
            // Calculate title text & button positions.
            float cw = Ui.ClientSize.Width / 2.0f;
            float offset = Ui.ClientSize.Height / 5.0f;

            _titleText.Position = new Vector2(cw - 4, offset - 4);
            _titleTextShadow.Position = new Vector2(cw, offset);

            _buttonPlay.Position = new Vector2(cw - _buttonPlay.Size.Width / 2, offset * 2 - _buttonPlay.Size.Height / 2);
            _buttonHighScores.Position = new Vector2(cw - _buttonHighScores.Size.Width / 2, offset * 3 - _buttonHighScores.Size.Height / 2);
            _buttonQuit.Position = new Vector2(cw - _buttonQuit.Size.Width / 2, offset * 4 - _buttonQuit.Size.Height / 2);

            AddFadeIn();
        }

        protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
        {
            if (_transitioning) return;

            foreach (var button in _buttons)
            {
                button.Hover = button.Bounds.Contains(e.Location);
            }
        }

        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            if (_transitioning) return;

            if (_buttonPlay.Bounds.Contains(e.Location))
            {
                _transitioning = true;
                AddFadeOut(() =>
                {
                    RemoveScreen(this);
                    AddScreen<BreakoutScreen>();
                });
            }
            else if (_buttonHighScores.Bounds.Contains(e.Location))
            {
                _transitioning = true;
                AddFadeOut(() =>
                {
                    RemoveScreen(this);
                    AddScreen<HighScoreScreen>();
                });
            }
            else if (_buttonQuit.Bounds.Contains(e.Location))
            {
                _transitioning = true;
                AddFadeOut(Ui.Close);
            }
        }

        protected override void OnUpdate()
        {
            if (_transitioning) return;

            foreach (var button in _buttons)
                button.Update();
        }

        protected override void OnDraw(Graphics g)
        {
            base.OnDraw(g);

            _titleTextShadow.Draw(g);
            _titleText.Draw(g);

            _buttonPlay.Draw(g);
            _buttonHighScores.Draw(g);
            _buttonQuit.Draw(g);
        }
    }
}
