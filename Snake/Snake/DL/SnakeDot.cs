using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Snake.DL
{
    public class SnakeDot
    {
        public int Width { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public SnakeDot(int x,int y)
        {
            Width = Globals.Width;
            PosX = x;
            PosY = y;
        }
    }
}
