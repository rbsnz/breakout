using System;

namespace Assignment2Prj
{
    class GameManager
    {
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
