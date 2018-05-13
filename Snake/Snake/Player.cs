using Snake.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public static class Player
    {
        /// <summary>
        /// 0 = Right;
        /// 1 = Down;
        /// 2 = Left;
        /// 3 = Up;
        /// </summary>
        public static int lastdir = 0;
        private static int nextdir = -1;
        /// <summary>
        /// Wahrscheinlichkeit in % auf random richtungswechsel
        /// </summary>
        private static int randomChange = 0;
        public static int ChooseDir(DL.Snake snake,Food food)
        {
            Random rnd = new Random();
            if (nextdir >= 0)
            {
                //randomChange++;
                int tmp = nextdir;
                nextdir = -1;
                lastdir = tmp;
                return tmp;
            }

            List<int> Options = new List<int> { 0,1,2,3 };
            int dir = 0;

            if(lastdir == 0)
            {
                Options.Remove(2);
            }
            else if(lastdir == 1)
            {
                Options.Remove(3);
            }
            else if (lastdir == 2)
            {
                Options.Remove(0);
            }
            else if (lastdir == 3)
            {
                Options.Remove(1);
            }

            for (int i = 1; i < snake.Elements.Count; i++)
            {
                if(!((snake.Elements[i].PosX - snake.Elements[0].PosX) < Globals.Width  +1&& ((snake.Elements[i].PosY - snake.Elements[0].PosY) < Globals.Width|| ( snake.Elements[0].PosY - snake.Elements[i].PosY) < Globals.Width)))
                {
                    Options.Remove(0);
                }
                if (!((snake.Elements[i].PosY - snake.Elements[0].PosY) < Globals.Width +1 && ((snake.Elements[i].PosX - snake.Elements[0].PosX) < Globals.Width|| (snake.Elements[0].PosX - snake.Elements[i].PosX) < Globals.Width)))
                {
                    Options.Remove(1);
                }
                if(!((snake.Elements[0].PosX - snake.Elements[i].PosX) < Globals.Width  +1&& ((snake.Elements[i].PosY - snake.Elements[0].PosY) < Globals.Width|| (snake.Elements[0].PosY - snake.Elements[i].PosY) < Globals.Width)))
                {
                    Options.Remove(2);
                }
                if(!((snake.Elements[0].PosY - snake.Elements[i].PosY) < Globals.Width +1&& ((snake.Elements[i].PosX - snake.Elements[0].PosX) < Globals.Width|| (snake.Elements[0].PosX - snake.Elements[i].PosX) < Globals.Width)))
                {
                    Options.Remove(3);
                }
            }

            if (food.posX > snake.Elements[0].PosX && Options.Contains(0))
            {
                dir = 0;
            }
            else if (food.posY > snake.Elements[0].PosY && Options.Contains(1))
            {
                dir = 1;
            }
            else if (food.posX < snake.Elements[0].PosX && Options.Contains(2))
            {
                dir = 2;
            }
            else if (food.posY < snake.Elements[0].PosY && Options.Contains(3))
            {
                dir = 3;
            }
            else
            {

                if (Options.Contains(lastdir - 1))
                {
                    dir = lastdir - 1;
                    nextdir = dir - 1;
                }
                else if (Options.Contains(lastdir + 1))
                {
                    dir = lastdir + 1;
                    nextdir = dir + 1;
                }
                else if (Options.Contains(lastdir))
                {
                    dir = lastdir;
                    nextdir = dir + 1;
                }
                else
                {
                    dir = Options[rnd.Next(0, Options.Count)];
                }
            }

            if (food.posX >= snake.Elements[0].PosX && food.posY >= snake.Elements[0].PosY)
            {
                if((food.posX - snake.Elements[0].PosX) > (food.posY- snake.Elements[0].PosY))
                {
                    if (Options.Contains(0))
                    {
                        dir = 0;
                    }
                    else if (Options.Contains(1))
                    {
                        dir = 1;
                    }
                    else
                    {
                        //dir = Options[rnd.Next(0, Options.Count)];
                        //nextdir = dir;
                    }
                    
                }
                else if ((food.posX - snake.Elements[0].PosX) < (food.posY - snake.Elements[0].PosY))
                {
                    if (Options.Contains(1))
                    {
                        dir = 1;
                    }
                    else if (Options.Contains(0))
                    {
                        dir = 0;
                    }
                    else
                    {
                        //nextdir = dir;
                        //dir = Options[rnd.Next(0, Options.Count)];
                    }
                }
                else
                {
                    //dir = Options[rnd.Next(0, Options.Count)];
                    //nextdir = dir;
                }
            }
            else if(food.posX >= snake.Elements[0].PosX && food.posY <= snake.Elements[0].PosY)
            {
                if((food.posX - snake.Elements[0].PosX) > (snake.Elements[0].PosY - food.posY))
                {
                    if (Options.Contains(0))
                    {
                        dir = 0;
                    }
                    else if (Options.Contains(3))
                    {
                        dir = 3;
                    }
                    else
                    {
                        //dir = Options[rnd.Next(0, Options.Count)];
                        //nextdir = dir;
                    }
                    
                }
                else if ((food.posX - snake.Elements[0].PosX) < (snake.Elements[0].PosY - food.posY))
                {
                    if (Options.Contains(3))
                    {
                        dir = 3;
                    }
                    else if (Options.Contains(0))
                    {
                        dir = 0;
                    }
                    else
                    {
                        //dir = Options[rnd.Next(0, Options.Count)];
                        //nextdir = dir;
                    }
                }
                else
                {
                    //dir = Options[rnd.Next(0, Options.Count)];
                    //nextdir = dir;
                }
            }
            else if (food.posX <= snake.Elements[0].PosX && food.posY >= snake.Elements[0].PosY)
            {
                if((snake.Elements[0].PosX - food.posX) > (food.posY - snake.Elements[0].PosY))
                {
                    if (Options.Contains(2))
                    {
                        dir = 2;
                    }
                    else if (Options.Contains(1))
                    {
                        dir = 1;
                    }
                    else
                    {
                        //dir = Options[rnd.Next(0, Options.Count)];
                        //nextdir = dir;
                    }
                }
                else if ((snake.Elements[0].PosX - food.posX) < (food.posY - snake.Elements[0].PosY))
                {
                    if (Options.Contains(1))
                    {
                        dir = 1;
                    }
                    else if (Options.Contains(2))
                    {
                        dir = 2;
                    }
                    else
                    {
                        //dir = Options[rnd.Next(0, Options.Count)];
                        //nextdir = dir;
                    }
                }
                else
                {
                    //dir = Options[rnd.Next(0, Options.Count)];
                    //nextdir = dir;
                }
            }
            else if (food.posX <= snake.Elements[0].PosX && food.posY <= snake.Elements[0].PosY)
            {
                if((snake.Elements[0].PosX - food.posX) > (snake.Elements[0].PosY - food.posY))
                {
                    if (Options.Contains(2))
                    {
                        dir = 2;
                    }
                    else if (Options.Contains(3))
                    {
                        dir = 3;
                    }
                    else
                    {
                        //dir = Options[rnd.Next(0, Options.Count)];
                        //nextdir = dir;
                    }
                }
                else if ((snake.Elements[0].PosX - food.posX) < (snake.Elements[0].PosY - food.posY))
                {
                    if (Options.Contains(3))
                    {
                        dir = 3;
                    }
                    else if (Options.Contains(2))
                    {
                        dir = 2;
                    }
                    else
                    {
                        //dir = Options[rnd.Next(0, Options.Count)];
                        //nextdir = dir;
                    }
                }
                else
                {
                    //dir = Options[rnd.Next(0, Options.Count)];
                    //nextdir = dir;
                }
            }

            if (rnd.Next(0, 100) < randomChange)
            {
                dir = Options[rnd.Next(0, Options.Count)];
            }


            lastdir = dir;
            return dir;
        }
    }
}
