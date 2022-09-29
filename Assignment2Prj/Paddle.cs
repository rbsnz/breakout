using System;
using System.Drawing;
using System.Windows.Forms;

namespace Assignment2Prj
{
    class Paddle : IRenderable
    {
        private int _paddleSpeed;

        public PointF Position { get; set; }
        public SizeF Size { get; set; }

        public Paddle(int paddleSpeed)
        {
            _paddleSpeed = paddleSpeed;
        }

        public void Render(Graphics g)
        {
            RectangleF rect = new RectangleF(Position, Size);
            rect.X -= Size.Width / 2; rect.Y -= Size.Height / 2;
            g.FillRectangle(Brushes.White, rect);
        }
    }
}
