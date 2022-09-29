using System;
using System.Drawing;
using System.Windows.Forms;

namespace Assignment2Prj
{
    class Paddle
    {
        private int _paddleSpeed;

        public PointF Position { get; set; }

        public Paddle(int paddleSpeed)
        {
            _paddleSpeed = paddleSpeed;
        }
    }
}
