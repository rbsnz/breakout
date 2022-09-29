using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment2Prj
{
    class Bricks
    {
        /// <summary>
        /// Your Brick class must have following attributes 
        /// Add more attributes if required 
        /// </summary>
        private PictureBox[,] _bricks;
        private int _rows;
        private int _cols;

        public Bricks(int rows, int cols)
        {
            _rows = rows;
            _cols = cols;
            _bricks = new PictureBox[rows, cols];
        }
        //Add methods 
    }
}
