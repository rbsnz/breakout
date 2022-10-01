using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2Prj
{
    public class Brick : IRenderable
    {
        public PointF Position { get; set; }
        public SizeF Size { get; set; }
        public RectangleF Area => new RectangleF(
            Position.X - Size.Width / 2,
            Position.Y - Size.Height / 2,
            Size.Width, Size.Height
        );

        public void Render(Graphics g)
        {
            g.FillRectangle(Brushes.LightCoral, Area);
            g.DrawRectangle(Pens.Maroon, Area);
        }
    }
}
