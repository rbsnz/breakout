using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;

using Breakout.Services;

namespace Breakout.Game
{
    /// <summary>
    /// Manages the game. Provides input to, updates, and displays game screens.
    /// </summary>
    public class GameManager : IDrawable
    {
        // Store a list of game screens.
        private LinkedList<GameScreen> _screens = new LinkedList<GameScreen>();

        /// <summary>
        /// Gets the user interface manager.
        /// </summary>
        public IUiManager Ui { get; }
        /// <summary>
        /// Gets the font manager.
        /// </summary>
        public IFontManager Font { get; }
        /// <summary>
        /// Gets the sound manager.
        /// </summary>
        public ISoundManager Sound { get; }
        /// <summary>
        /// Gets the high score manager.
        /// </summary>
        public IHighScores HighScores { get; }

        /// <summary>
        /// Constructs a new game manager with the specified services.
        /// </summary>
        public GameManager(
            IUiManager uiManager,
            IFontManager fontManager,
            ISoundManager soundManager,
            IHighScores highScores)
        {
            Ui = uiManager;
            Sound = soundManager;
            Font = fontManager;
            HighScores = highScores;

            Ui.ClientSize = new Size(
                (Theme.BrickColumns * Theme.BrickWidth + 2) * Theme.BrickUnit,
                (Theme.BrickRows + 22) * Theme.BrickUnit
            );
        }

        // Screen management

        /// <summary>
        /// Enumerates the screens, allowing a screen to remove itself or others during the enumeration.
        /// </summary>
        private IEnumerable<GameScreen> EnumerateScreens()
        {
            var node = _screens.First;
            while (node != null)
            {
                yield return node.Value;
                node = node.Next;
            }
        }

        /// <summary>
        /// Adds the specified screen.
        /// </summary>
        public void AddScreen(GameScreen screen)
        {
            _screens.AddLast(screen);
            screen.HandleAdd();
        }

        /// <summary>
        /// Adds a new screen of the specified type and optionally configures it upon creation with an action.
        /// </summary>
        /// <returns>The newly created screen.</returns>
        public T AddScreen<T>(Action<T> configure) where T : GameScreen
        {
            var constructor = typeof(T).GetConstructor(new Type[] { typeof(GameManager) });
            if (constructor is null)
                throw new InvalidOperationException($"The screen must have a constructor that accepts a single GameManager parameter.");

            T screen = (T)constructor.Invoke(new object[] { this });

            if (!(configure is null))
                configure(screen);

            AddScreen(screen);
            return screen;
        }

        /// <summary>
        /// Adds a new screen of the specified type.
        /// </summary>
        /// <returns>The newly created screen.</returns>
        public T AddScreen<T>() where T : GameScreen => AddScreen<T>(null);

        /// <summary>
        /// Adds a fade in screen an optionally specifies an action to be invoked after the fade has completed.
        /// </summary>
        public void AddFadeIn(Action onComplete = null) => AddScreen<FadeIn>(x => x.OnComplete = onComplete);

        /// <summary>
        /// Adds a fade out screen an optionally specifies an action to be invoked after the fade has completed.
        /// </summary>
        public void AddFadeOut(Action onComplete = null) => AddScreen<FadeOut>(x => x.OnComplete = onComplete);

        /// <summary>
        /// Gets the screen of the specified type if it exists, or <see langword="null"/> if it was not found.
        /// </summary>
        public T GetScreen<T>() where T : GameScreen => _screens.OfType<T>().FirstOrDefault();

        /// <summary>
        /// Removes the specified screen.
        /// </summary>
        /// <returns><see langword="true"/> if the screen was removed.</returns>
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

        /// <summary>
        /// Removes the screen of the specified type.
        /// </summary>
        /// <returns><see langword="true"/> if the screen was removed.</returns>
        public bool RemoveScreen<T>() where T : GameScreen => RemoveScreen(GetScreen<T>());

        // Handles user input and sends it to each screen.
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

        /// <summary>
        /// Updates each screen.
        /// </summary>
        public void Update()
        {
            foreach (var screen in EnumerateScreens())
                screen.Update();
        }

        /// <summary>
        /// Draws each screen to the specified render target.
        /// </summary>
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
