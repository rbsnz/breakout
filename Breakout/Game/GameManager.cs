using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;

using Breakout.Audio;
using Breakout.Fonts;
using Breakout.Data;
using Breakout.Services;

namespace Breakout.Game
{
    public class GameManager : IDrawable
    {
        private readonly Vector2 _stageSize;
        private readonly RectangleF _stageArea;

        private LinkedList<GameScreen> _screens = new LinkedList<GameScreen>();

        public IUiManager Ui { get; }
        public FontManager Font { get; }
        public ISoundManager Sound { get; }

        private TitleScreen _menuScreen;
        private BreakoutScreen _breakoutScreen;
        private PauseScreen _pauseScreen;

        public GameManager(IUiManager uiManager, ISoundManager soundManager)
        {
            Ui = uiManager;
            Sound = soundManager;
            Font = new FontManager(@"res\font");

            Ui.ClientSize = new Size(
                (Theme.BrickColumns * Theme.BrickWidth + 2) * Theme.BrickUnit,
                (Theme.BrickRows + 22) * Theme.BrickUnit
            );

            _stageArea = new RectangleF(0, 0, _stageSize.X, _stageSize.Y);
        }

        // Screens
        private IEnumerable<GameScreen> EnumerateScreens()
        {
            var node = _screens.First;
            while (node != null)
            {
                yield return node.Value;
                node = node.Next;
            }
        }

        public void AddScreen(GameScreen screen)
        {
            _screens.AddLast(screen);
            screen.HandleAdd();
        }
        public T GetScreen<T>() where T : GameScreen => _screens.OfType<T>().FirstOrDefault();
        public bool RemoveScreen(GameScreen screen)
        {
            if (_screens.Remove(screen))
            {
                screen.HandleRemove();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Initialize()
        {

        }

        internal void HandleMouseMove(MouseEventArgs e)
        {
            foreach (var screen in EnumerateScreens())
                screen.HandleMouseMove(e);
        }

        internal void HandleMouseDown(MouseEventArgs e)
        {
            foreach (var screen in EnumerateScreens())
                screen.HandleMouseDown(e);
        }

        internal void HandleMouseUp(MouseEventArgs e)
        {
            foreach (var screen in EnumerateScreens())
                screen.HandleMouseUp(e);
        }

        internal void HandleKeyDown(KeyEventArgs e)
        {
            foreach (var screen in EnumerateScreens())
                screen.HandleKeyDown(e);
        }

        internal void HandleKeyUp(KeyEventArgs e)
        {
            foreach (var screen in EnumerateScreens())
                screen.HandleKeyUp(e);
        }

        public void Update()
        {
            foreach (var screen in EnumerateScreens())
                screen.Update();
        }

        public void Draw(Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            g.Clear(Color.Black);

            foreach (var screen in _screens)
                screen.Draw(g);
        }
    }
}
