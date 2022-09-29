using System;
using System.Windows.Forms;

namespace Assignment2Prj
{
    class Paddle
    {
        private PictureBox _picPaddle;
        private int _paddleSpeed;

        public Paddle(PictureBox picPaddle, int paddleSpeed)
        {
            _picPaddle = picPaddle;
            _paddleSpeed = paddleSpeed;
        }
    }
}
