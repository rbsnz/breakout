using System;
using System.Drawing;
using System.Numerics;

namespace Breakout.Game
{
    /// <summary>
    /// Represents an expanding text indicating the score obtained when a brick is hit.
    /// </summary>
    public class HitScore : IDrawable
    {
        private readonly Text _text;
        private float _alpha;

        /// <summary>
        /// Gets the position of this hit score.
        /// </summary>
        public Vector2 Position { get; }

        /// <summary>
        /// Gets the scale of this hit score.
        /// </summary>
        public float Scale { get; private set; }

        /// <summary>
        /// Gets the opacity of this hit score.
        /// </summary>
        public float Opacity
        {
            get => _alpha;
            private set
            {
                _alpha = Math.Max(0, Math.Min(1, value));
                _text.Color = Color.FromArgb((int)(255 * Opacity), _text.Color);
            }
        }

        /// <summary>
        /// Constructs a new hit score with the specified position, text and color.
        /// </summary>
        public HitScore(GameManager manager, Vector2 position, string text, Color color)
        {
            _text = new Text(manager, manager.Font.GetFont(Theme.FontFamily, 20.0f))
            {
                Value = text,
                Color = color
            };

            Position = position;
            Scale = 1.0f;
            Opacity = 1.0f;
        }

        /// <summary>
        /// Updates this hit score and returns whether it is still alive or not.
        /// </summary>
        public bool Update()
        {
            Scale *= 1.05f;
            Opacity = 1.0f - (Scale / 3);
            return Opacity > 0;
        }

        /// <summary>
        /// Draws this hit score to the specified render target.
        /// </summary>
        public void Draw(Graphics g)
        {
            g.TranslateTransform(Position.X, Position.Y);
            g.ScaleTransform(Scale, Scale);
            _text.Draw(g);
            g.ResetTransform();
        }
    }
}
