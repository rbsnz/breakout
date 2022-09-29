using System;
using System.Windows.Forms;

namespace Assignment2Prj
{
    class Ball
    {
        private PictureBox _picBall;
        private int _verticalSpeed, _horizontalSpeed;

        public Ball(PictureBox picBall, int verticalSpeed, int horizontalSpeed)
        {
            _picBall = picBall;
            _verticalSpeed = verticalSpeed;
            _horizontalSpeed = horizontalSpeed;
        }
    }
}
