using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2Prj
{
    class Mannger
    {
        /// <summary>
        /// Your Manager class must have following attributes 
        /// Add more attributes if required 
        /// </summary>
        private Bricks bricks;
        private Ball ball;
        private Paddle paddle;

        public Mannger(Bricks bricks, Ball ball, Paddle paddle)
        {
            this.bricks = bricks;
            this.ball = ball;
            this.paddle = paddle;


        }
    }
}
