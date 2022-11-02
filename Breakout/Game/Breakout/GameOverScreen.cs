using System.Drawing;

namespace Breakout.Game
{
    public class GameOverScreen : GameScreen
    {
        private readonly Font _font;

        private readonly Dimmer _dimmer;

        private readonly Text _gameOverText;

        public GameOverScreen(GameManager manager)
            : base(manager)
        {
            _font = Font.GetFont(Theme.FontFamily, 20.0f);
            _dimmer = new Dimmer();

            _gameOverText = new Text(Manager, _font)
            {
                Value = "GAME OVER!",
                Color = Color.Magenta,
                Position = manager.Ui.ClientSize.ToVector2() / 2
            };
        }

        protected override void OnUpdate()
        {
            _dimmer.Update();
        }

        protected override void OnDraw(Graphics g)
        {
            _dimmer.Draw(g);
            _gameOverText.Draw(g);
        }
    }
}
