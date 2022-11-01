﻿using System;
using System.Drawing;
using System.Windows.Forms;

using Breakout.Fonts;
using Breakout.Services;

namespace Breakout.Game
{
    /// <summary>
    /// Represents a game screen that receives input, updates, and draws.
    /// </summary>
    public abstract class GameScreen : IDisposable
    {
        protected bool _disposed;

        public GameManager Manager { get; }
        public IUiManager Ui => Manager.Ui;
        public FontManager Font => Manager.Font;
        public ISoundManager Sound => Manager.Sound;

        public bool IsActive { get; set; } = true;

        protected GameScreen(GameManager manager)
        {
            Manager = manager;
        }

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

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            _disposed = true;

            if (disposing)
            {
                OnRemove();
            }
        }

        public void Dispose() => Dispose(true);
    }
}