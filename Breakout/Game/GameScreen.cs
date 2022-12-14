using System;
using System.Drawing;
using System.Windows.Forms;

using Breakout.Services;

namespace Breakout.Game
{
    /// <summary>
    /// Represents a game screen that may receive input, update, and draw to the screen.
    /// </summary>
    public abstract class GameScreen
    {
        // Store a reference to the game manager.
        public GameManager Manager { get; }

        // Defines properties/methods that proxy to the game manager for convenience.
        public IUiManager Ui => Manager.Ui;
        public IFontManager Font => Manager.Font;
        public ISoundManager Sound => Manager.Sound;
        public IHighScores HighScores => Manager.HighScores;

        protected void AddScreen(GameScreen screen) => Manager.AddScreen(screen);
        protected void AddScreen<T>(Action<T> configure = null) where T : GameScreen => Manager.AddScreen<T>(configure);
        protected void AddFadeIn(Action onComplete = null) => Manager.AddFadeIn(onComplete);
        protected void AddFadeOut(Action onComplete = null) => Manager.AddFadeOut(onComplete);
        protected bool RemoveScreen(GameScreen screen) => Manager.RemoveScreen(screen);
        protected bool RemoveScreen<T>() where T : GameScreen => Manager.RemoveScreen<T>();

        protected GameScreen(GameManager manager)
        {
            Manager = manager;
        }

        // Define events with protected virtual methods that may be overridden by subclasses.

        internal void HandleAdd() => OnAdd();
        protected virtual void OnAdd() { }
        internal void HandleRemove() => OnRemove();
        protected virtual void OnRemove() { }

        public void HandleMouseMove(MouseEventArgs e) => OnMouseMove(e);
        protected virtual void OnMouseMove(MouseEventArgs e) { }

        public void HandleMouseDown(MouseEventArgs e) => OnMouseDown(e);
        protected virtual void OnMouseDown(MouseEventArgs e) { }

        public void HandleMouseUp(MouseEventArgs e) => OnMouseUp(e);
        protected virtual void OnMouseUp(MouseEventArgs e) { }

        public void HandleKeyDown(KeyEventArgs e) => OnKeyDown(e);
        protected virtual void OnKeyDown(KeyEventArgs e) { }

        public void HandleKeyUp(KeyEventArgs e) => OnKeyUp(e);
        protected virtual void OnKeyUp(KeyEventArgs e) { }

        public void Update() => OnUpdate();
        protected virtual void OnUpdate() { }

        public void Draw(Graphics g) => OnDraw(g);
        protected virtual void OnDraw(Graphics g) { }
    }
}
