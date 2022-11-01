﻿using System;
using System.Drawing;
using System.Numerics;

namespace Breakout.Game
{
    public class Button : IDrawable
    {
        public Font Font { get; }
        public string Text { get; }

        public bool IsVisible { get; set; }
        public bool IsEnabled { get; set; }

        public Vector2 Position { get; set; }
        public SizeF Size { get; }

        public RectangleF Bounds => new RectangleF(Position.ToPointF(), Size);

        public bool Hover { get; set; }

        private float _transition = 0.0f;

        public Button(GameManager manager, Font font, string text)
        {
            Font = font;
            Text = text;

            Size = manager.Ui.MeasureString(Text, Font);
        }

        public void Update()
        {
            float target = Hover ? 1 : 0;
            _transition += (target - _transition) * 0.3f;
        }

        public void Draw(Graphics g)
        {
            g.DrawString(Text, Font, Brushes.Magenta, Position.ToPointF());
            g.DrawString(Text, Font, Brushes.Yellow, (Position - new Vector2(4, 4) * _transition).ToPointF());
        }
    }
}