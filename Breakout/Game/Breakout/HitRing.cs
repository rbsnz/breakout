using System.Drawing;
using System.Numerics;

namespace Breakout.Game
{
    /// <summary>
    /// Represents an expanding ring indicating the start of a new combo.
    /// </summary>
    public class HitRing : IDrawable
    {
        private float _opacity;
        private Brush _brush;

        /// <summary>
        /// Gets the position of this hit ring.
        /// </summary>
        public Vector2 Position { get; }

        /// <summary>
        /// Gets the radius of this hit ring.
        /// </summary>
        public float Radius { get; }

        /// <summary>
        /// Gets the color of this hit ring.
        /// </summary>
        public Color Color { get; }
       
        /// <summary>
        /// Gets the scale of this hit ring.
        /// </summary>
        public float Scale { get; private set; } = 1.0f;

        /// <summary>
        /// Gets the opacity of this hit ring.
        /// </summary>
        public float Opacity
        {
            get => _opacity;
            private set
            {
                _opacity = value;
                _brush = new SolidBrush(Color.WithOpacity(Opacity));
            }
        }

        /// <summary>
        /// Constructs a new hit ring with the specified position, radius and color.
        /// </summary>
        public HitRing(Vector2 position, float radius, Color color)
        {
            Position = position;
            Radius = radius;
            Color = color;
            Opacity = 1.0f;
        }

        /// <summary>
        /// Updates this hit ring and returns whether it is still alive or not.
        /// </summary>
        public bool Update()
        {
            Scale *= 1.05f;
            Scale *= 1.05f;
            Opacity = 1.0f - Scale / 8;
            return Opacity > 0;
        }

        /// <summary>
        /// Draws this hit ring to the specified render target.
        /// </summary>
        public void Draw(Graphics g)
        {
            g.FillCircle(_brush, Position, Radius * Scale);
        }
    }
}
