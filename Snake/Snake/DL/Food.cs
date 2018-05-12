using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.DL
{
    public class Food
    {
        public int posX { get; set; }
        public int posY { get; set; }
        public int width { get; set; }
        public Food(int x,int y)
        {
            posX = x;
            posY = y;
            width = Globals.Width;
        }
    }
}
