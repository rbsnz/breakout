﻿using System;
using System.Drawing;
using System.Numerics;
using Breakout.Fonts;
using Breakout.Data;

namespace Breakout.Game
{
    /// <summary>
    /// A text that floats indicating the score obtained when a brick is hit.
    /// </summary>
    public class HitScore : IDrawable
    {
        private readonly Text _text;
        private float _alpha;

        public Vector2 Position { get; }
        public float Scale { get; set; }
        public float Alpha
        {
            get => _alpha;
            set
            {
                _alpha = Math.Max(0, Math.Min(1, value));
                _text.Color = Color.FromArgb((int)(255 * Alpha), _text.Color);
            }
        }

        public HitScore(GameManager manager, Vector2 position, string text, Color color)
        {
            _text = new Text(manager, manager.Font.GetFont(Theme.FontFamily, 20.0f))
            {
                Value = text,
                Color = color
            };

            Position = position;
            Scale = 1.0f;
            Alpha = 1.0f;
        }

        public bool Update()
        {
            Scale *= 1.05f;
            Alpha = 1.0f - (Scale / 3);
            return Alpha > 0;
        }

        public void Draw(Graphics g)
        {
            g.TranslateTransform(Position.X, Position.Y);
            g.ScaleTransform(Scale, Scale);
            _text.Draw(g);
            g.ResetTransform();
        }
    }
}
