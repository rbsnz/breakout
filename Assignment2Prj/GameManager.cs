using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2Prj
{
    class GameManager
    {
        /// <summary>
        /// Your Manager class must have following attributes 
        /// Add more attributes if required 
        /// </summary>
        private Bricks _bricks;
        private Ball _ball;
        private Paddle _paddle;

        public GameManager(Bricks bricks, Ball ball, Paddle paddle)
        {
            _bricks = bricks;
            _ball = ball;
            _paddle = paddle;
        }
    }
}
