using System;
using System.Drawing;
using System.Windows.Forms;

namespace Assignment2Prj
{
    class Ball : IRenderable
    {
        public PointF Position { get; set; }
        public PointF Direction { get; set; } = new PointF(1, 1);
        public float Velocity { get; set; } = 4.0f;
        public float Radius { get; set; } = 8.0f;
        public RectangleF Rect => new RectangleF(Position.X - Radius, Position.Y - Radius, Radius * 2, Radius * 2);

        public Ball() { }

        public void Render(Graphics g)
        {
            g.FillEllipse(Brushes.White, Position.X - Radius, Position.Y - Radius, Radius * 2, Radius * 2);
        }
    }
}
