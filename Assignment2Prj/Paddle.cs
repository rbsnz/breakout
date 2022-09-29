using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment2Prj
{
    class Paddle
    {
        /// <summary>
        /// Your Paddle class must have following attributes 
        /// Add more attributes if required 
        /// </summary>
        private PictureBox _picPaddle;
        private int _paddleSpeed;

        public Paddle(PictureBox picPaddle, int paddleSpeed)
        {
            _picPaddle = picPaddle;
            _paddleSpeed = paddleSpeed;
        }
    }
}
