
using System.Drawing;
using System.Numerics;

namespace Breakout.Game
{
    public class TitleScreen : GameScreen
    {
        private Font _titleFont;

        private PointF _titlePosition;

        private Button _buttonPlay, _buttonHighScores, _buttonQuit;

        private Button[] _buttons;

        private bool _transitioning;

        public TitleScreen(GameManager manager)
            : base(manager)
        {
            _titleFont = Font.GetFont(Theme.FontFamily, 36.0f);

            _buttonPlay = new Button(Manager, _titleFont, "Play");
            _buttonHighScores = new Button(Manager, _titleFont, "High Scores");
            _buttonQuit = new Button(Manager, _titleFont, "Quit");

            _buttons = new[] { _buttonPlay, _buttonHighScores, _buttonQuit };
        }

        protected override void OnAdd()
        {
            float cw = Ui.ClientSize.Width / 2.0f;
            float offset = Ui.ClientSize.Height / 5.0f;
            _titlePosition = new PointF(cw, offset);

            _buttonPlay.Position = new Vector2(cw - _buttonPlay.Size.Width / 2, offset * 2 - _buttonPlay.Size.Height / 2);
            _buttonHighScores.Position = new Vector2(cw - _buttonHighScores.Size.Width / 2, offset * 3 - _buttonHighScores.Size.Height / 2);
            _buttonQuit.Position = new Vector2(cw - _buttonQuit.Size.Width / 2, offset * 4 - _buttonQuit.Size.Height / 2);
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
                Manager.AddScreen(new FadeOut(Manager, () =>
                {
                    Manager.RemoveScreen(this);
                    Manager.AddScreen<BreakoutScreen>();
                }));
            }
            else if (_buttonHighScores.Bounds.Contains(e.Location))
            {
                _transitioning = true;
                AddScreen(new FadeOut(Manager, () =>
                {
                    RemoveScreen(this);
                    AddScreen<HighScoreScreen>();
                    AddScreen<FadeIn>();
                }));
            }
            else if (_buttonQuit.Bounds.Contains(e.Location))
            {
                _transitioning = true;
                AddScreen(new FadeOut(Manager, () =>
                {
                    Ui.Close();
                }));
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

            g.DrawString("BREAKOUT", _titleFont, Brushes.Magenta,
                _titlePosition,
                StringFormats.TopCenter
            );

            g.DrawString("BREAKOUT", _titleFont, Brushes.Yellow,
                new PointF(_titlePosition.X - 4, _titlePosition.Y - 4),
                StringFormats.TopCenter
            );

            _buttonPlay.Draw(g);
            _buttonHighScores.Draw(g);
            _buttonQuit.Draw(g);
        }
    }
}
