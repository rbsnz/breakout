using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

using Breakout.Data;

namespace Breakout.Game
{
    public class GameOverScreen : GameScreen
    {
        private readonly int _score;

        private readonly Font _font;
        private readonly Dimmer _dimmer;
        private readonly Text _gameOverText;
        private readonly Text _nameText;

        private readonly bool _isHighScore;

        private bool _transitioning;

        private string _name = "";


        public GameOverScreen(GameManager manager, int score)
            : base(manager)
        {
            _font = Font.GetFont(Theme.FontFamily, 20.0f);
            _dimmer = new Dimmer();
            _score = score;

            _isHighScore = manager.HighScores.IsHighScore(_score);

            string gameOverString = "GAME OVER!";
            if (_isHighScore)
            {
                gameOverString += "\n\nYou got a high score.\nEnter your name:\n";
            }
            else
            {
                gameOverString += "\n\nUnforunately you didn't make\nthe high score list.\n\nPress any key to continue.";
            }

            _gameOverText = new Text(Manager, _font)
            {
                Value = gameOverString,
                Color = Color.Magenta,
                Position = manager.Ui.ClientSize.ToVector2() / 2
            };

            _nameText = new Text(Manager, _font)
            {
                Value = "_",
                Color = Color.Cyan,
                Position = new Vector2(
                    _gameOverText.Position.X,
                    _gameOverText.Position.Y + _gameOverText.Size.Height + 10
                ),
                Alignment = ContentAlignment.TopCenter
            };
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (_transitioning) return;

            if (!_isHighScore)
            {
                _transitioning = true;
                AddFadeOut(() =>
                {
                    RemoveScreen<BreakoutScreen>();
                    RemoveScreen(this);
                    AddScreen<HighScoreScreen>();
                });
                return;
            }

            if ((e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z) ||
                (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9))
            {
                if (_name.Length < 15)
                {
                    _name += (char)e.KeyValue;
                    _nameText.Value = $"{_name}_";
                }
            }

            if (e.KeyCode == Keys.Back)
            {
                if (_name.Length > 0)
                {
                    _name = _name.Substring(0, _name.Length - 1);
                    _nameText.Value = $"{_name}_";
                }
            }

            if (e.KeyCode == Keys.Return)
            {
                if (!string.IsNullOrWhiteSpace(_name))
                {
                    _transitioning = true;
                    Manager.HighScores.Add(new HighScore(_score, _name));
                    AddFadeOut(() =>
                    {
                        RemoveScreen<BreakoutScreen>();
                        RemoveScreen(this);
                        AddScreen<HighScoreScreen>();
                    });
                }
            }
        }

        protected override void OnUpdate()
        {
            _dimmer.Update();
        }

        protected override void OnDraw(Graphics g)
        {
            _dimmer.Draw(g);
            _gameOverText.Draw(g);
            if (_isHighScore)
                _nameText.Draw(g);
        }
    }
}
