using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake.DL
{
    public class Snake
    {
        public List<SnakeDot> Elements { get; set; }
        /// <summary>
        /// 0 = Right;
        /// 1 = Down;
        /// 2 = Left;
        /// 3 = Up;
        /// </summary>
        public int dir { get; set; } 
        public int level { get; set; }
        private List<Color> colors = new List<Color> { Color.Gray, Color.Goldenrod, Color.White, Color.Yellow, Color.Gold, Color.Thistle, Color.SteelBlue, Color.WhiteSmoke, Color.RoyalBlue, Color.PeachPuff,Color.Green,Color.Green,Color.LawnGreen,Color.Pink,Color.Purple,Color.Olive,Color.LimeGreen,Color.LightSteelBlue,Color.LightGoldenrodYellow,Color.Ivory,Color.HotPink,Color.DeepPink };
        public Snake()
        {
            dir = 0;
            level = 1;
            Elements = new List<SnakeDot>();
        }
        public Color GetSnakeColor()
        {
            Random rnd = new Random();
            colors = colors.OrderBy(x => rnd.Next()).ToList();
            Color tmp = Color.White;

            if(level >= colors.Count)
            {
                tmp = colors[rnd.Next(0, colors.Count)];
                //tmp = Color.Green;
            }
            else
            {
                tmp = colors[level];
            }
            

            return tmp;
        }
        public void Shift()
        {
            while (dir > 3)
            {
                dir = dir - 4;
            }
            while (dir < 0)
            {
                dir = dir + 4;
            }
            //var tmp = Elements;
            for(int i = Elements.Count-1; i > 0;i--)
            {
                Elements[i].PosX = Elements[i - 1].PosX;
                Elements[i].PosY = Elements[i - 1].PosY;
            }
            
            if(dir == 0)
            {
                Elements[0].PosX = Elements[0].PosX + Globals.Width + 1;
            }
            else if(dir == 1)
            {
                Elements[0].PosY = Elements[0].PosY + Globals.Width + 1;
            }
            else if (dir == 2)
            {
                Elements[0].PosX = (Elements[0].PosX - Globals.Width) - 1;
            }
            else if (dir == 3)
            {
                Elements[0].PosY = (Elements[0].PosY - Globals.Width) - 1;
            }
        }
        public void Append()
        {
            SnakeDot last = Elements[Elements.Count - 1];
            int x=last.PosX;
            int y=last.PosY;
            if (dir == 0)
            {
                x = x - Globals.Width;
                x--;
            }
            else if(dir == 1)
            {
                y = y - Globals.Width;
                y--;
            }
            else if(dir == 2)
            {
                x = x + Globals.Width;
                x++;
            }
            else if (dir == 3)
            {
                y = y + Globals.Width;
                y++;
            }

            Elements.Add(new SnakeDot(x, y,GetSnakeColor()));
        }
    }
}
