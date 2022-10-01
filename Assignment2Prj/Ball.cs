using System;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

namespace Assignment2Prj
{
    class Ball : IRenderable
    {
        public PointF Position { get; set; }
        public Vector2 Velocity { get; set; }
        public float Radius { get; set; } = 8.0f;
        public RectangleF Rect => new RectangleF(Position.X - Radius, Position.Y - Radius, Radius * 2, Radius * 2);

        public Ball() { }

        public void Render(Graphics g)
        {
            g.FillEllipse(Brushes.White, Position.X - Radius, Position.Y - Radius, Radius * 2, Radius * 2);
        }
    }
}
