using System;
using System.Windows.Forms;

namespace Assignment2Prj
{
    class Bricks
    {
        private PictureBox[,] _bricks;
        private int _rows;
        private int _cols;

        public Bricks(int rows, int cols)
        {
            _rows = rows;
            _cols = cols;
            _bricks = new PictureBox[rows, cols];
        }
    }
}
