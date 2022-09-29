using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment2Prj
{
    class Ball
    {
        /// <summary>
        /// Your Ball class must have following attributes 
        /// Add more attributes if required 
        /// </summary>
        private PictureBox _picBall;
        private int _verticalSpeed, _horizontalSpeed;

        public Ball(PictureBox picBall, int verticalSpeed, int horizontalSpeed)
        {
            _picBall = picBall;
            _verticalSpeed = verticalSpeed;
            _horizontalSpeed = horizontalSpeed;
        }
        //Add methods

    }
}
