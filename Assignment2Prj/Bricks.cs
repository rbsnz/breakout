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
        private PictureBox[,] bricks;
        private int rows;
        private int cols;

        public Bricks(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;
            this.bricks = new PictureBox[rows, cols];
        }
        //Add methods 
    }
}
