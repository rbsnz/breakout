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
        private PictureBox picPaddle;
        private int paddleSpeed;

        public Paddle(PictureBox picPaddle, int paddleSpeed)
        {
            this.picPaddle = picPaddle;
            this.paddleSpeed = paddleSpeed;
        }
    }
}
