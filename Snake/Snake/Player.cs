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
        public static List<Tuple<int, int,List<int>>> Log = new List<Tuple<int, int,List<int>>>();
        private static List<int> lastOptions = new List<int>();
        public static List<Situation> Situations { get; set; }
        /// <summary>
        /// Wahrscheinlichkeit in % auf random richtungswechsel
        /// </summary>
        private static int randomChange = 0;
        private static void logging(int dir,int reason,List<int> options)
        {
            Log.Add(new Tuple<int, int, List<int>>(dir, reason, options));
        }
        public static void DownVoteLast()
        {
            Situation situation = GetSituationByOptions(lastOptions);
            if (situation != null)
            {
                Situation tmpSit = Situations[Situations.IndexOf(situation)];
                foreach (Tuple<int, int> tmp in tmpSit.Options)
                {
                    if (tmp.Item1 == lastdir)
                    {
                        Situations[Situations.IndexOf(situation)].Options[Situations[Situations.IndexOf(situation)].Options.IndexOf(tmp)] = new Tuple<int, int>(lastdir, 1);
                        break;
                    }
                }
            }
        }
        private static Situation GetSituationByOptions(List<int> options)
        {
            Situation situation = null;
            foreach (Situation s in Situations)
            {
                if (s.Options.Count == options.Count)
                {
                    bool mySituation = true;
                    for (int i = 0; i < s.Options.Count; i++)
                    {
                        if (s.Options[i].Item1 != options[i])
                        {
                            mySituation = false;
                        }
                    }
                    if (mySituation)
                    {
                        situation = s;
                        break;
                    }
                }
            }
            return situation;
        }
        public static int ChooseDir(DL.Snake snake,Food food)
        {
            if(lastOptions.Count > 0 && Game.alive)
            {
                Situation situation = GetSituationByOptions(lastOptions);
                if(situation != null)
                {
                    Situation tmpSit = Situations[Situations.IndexOf(situation)];
                    foreach (Tuple<int,int>tmp in tmpSit.Options)
                    {
                        if(tmp.Item1 == lastdir)
                        {
                            Situations[Situations.IndexOf(situation)].Options[Situations[Situations.IndexOf(situation)].Options.IndexOf(tmp)] = new Tuple<int, int>(lastdir, tmp.Item2 + 1);
                            break;
                        }
                    }
                }
            }
            Random rnd = new Random();
            if (nextdir >= 0)
            {
                //randomChange++;
                int tmp = nextdir;
                nextdir = -1;
                lastdir = tmp;
                logging(tmp, -1, null);
                return tmp;
            }

            List<int> Options = new List<int> { 0,1,2,3 };
            int dir = 0;

            if (lastdir == 0)
            {
                Options.Remove(2);
            }
            else if (lastdir == 1)
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
                //if(!((snake.Elements[i].PosX - snake.Elements[0].PosX) < Globals.Width  +1 && (((snake.Elements[i].PosY - snake.Elements[0].PosY) < Globals.Width && (snake.Elements[i].PosY - snake.Elements[0].PosY) > 0) || ((snake.Elements[0].PosY - snake.Elements[i].PosY) < Globals.Width && (snake.Elements[0].PosY - snake.Elements[i].PosY) > 0 ))))
                //{
                //    Options.Remove(0);
                //}
                //if (!((snake.Elements[i].PosY - snake.Elements[0].PosY) < Globals.Width +1 && (((snake.Elements[i].PosX - snake.Elements[0].PosX) < Globals.Width && (snake.Elements[i].PosX - snake.Elements[0].PosX) > 0) || ((snake.Elements[0].PosX - snake.Elements[i].PosX) < Globals.Width && (snake.Elements[0].PosX - snake.Elements[i].PosX) > 0))))
                //{
                //    Options.Remove(1);
                //}
                //if(!((snake.Elements[0].PosX - snake.Elements[i].PosX) < Globals.Width  +1 && (((snake.Elements[i].PosY - snake.Elements[0].PosY) < Globals.Width && (snake.Elements[i].PosY - snake.Elements[0].PosY) > 0) || ((snake.Elements[0].PosY - snake.Elements[i].PosY) < Globals.Width && (snake.Elements[0].PosY - snake.Elements[i].PosY) > 0))))
                //{
                //    Options.Remove(2);
                //}
                //if(!((snake.Elements[0].PosY - snake.Elements[i].PosY) < Globals.Width +1 && (((snake.Elements[i].PosX - snake.Elements[0].PosX) < Globals.Width && (snake.Elements[i].PosX - snake.Elements[0].PosX) > 0) || ((snake.Elements[0].PosX - snake.Elements[i].PosX) < Globals.Width && (snake.Elements[0].PosX - snake.Elements[i].PosX) > 0))))
                //{
                //    Options.Remove(3);
                //}

                if(snake.Elements[0].PosX > snake.Elements[i].PosX)
                {
                    if(snake.Elements[0].PosY == snake.Elements[i].PosY)
                    {
                        if (snake.Elements[0].PosX - snake.Elements[i].PosX == Globals.Width + 1)
                        {
                            Options.Remove(2);
                        }
                    }
                }
                else if(snake.Elements[0].PosX == snake.Elements[i].PosX)
                {
                    
                }
                else
                {
                    if (snake.Elements[0].PosY == snake.Elements[i].PosY)
                    {
                        if (snake.Elements[i].PosX - snake.Elements[0].PosX == Globals.Width + 1)
                        {
                            Options.Remove(0);
                        }
                    }
                }
                if (snake.Elements[0].PosY > snake.Elements[i].PosY)
                {
                    if (snake.Elements[0].PosX == snake.Elements[i].PosX)
                    {
                        if (snake.Elements[0].PosY - snake.Elements[i].PosY == Globals.Width + 1)
                        {
                            Options.Remove(3);
                        }
                    }
                }
                else if (snake.Elements[0].PosY == snake.Elements[i].PosY)
                {
                    
                }
                else
                {
                    if (snake.Elements[0].PosX == snake.Elements[i].PosX)
                    {
                        if (snake.Elements[i].PosY - snake.Elements[0].PosY <= Globals.Width + 1)
                        {
                            Options.Remove(1);
                        }
                    }
                }
            }

            //if (food.posX > snake.Elements[0].PosX && Options.Contains(0))
            //{
            //    dir = 0;
            //}
            //else if (food.posY > snake.Elements[0].PosY && Options.Contains(1))
            //{
            //    dir = 1;
            //}
            //else if (food.posX < snake.Elements[0].PosX && Options.Contains(2))
            //{
            //    dir = 2;
            //}
            //else if (food.posY < snake.Elements[0].PosY && Options.Contains(3))
            //{
            //    dir = 3;
            //}
            //else
            //{

            //    if (Options.Contains(lastdir - 1))
            //    {
            //        dir = lastdir - 1;
            //        //nextdir = dir - 1;
            //    }
            //    else if (Options.Contains(lastdir + 1))
            //    {
            //        dir = lastdir + 1;
            //        //nextdir = dir + 1;
            //    }
            //    else if (Options.Contains(lastdir))
            //    {
            //        dir = lastdir;
            //        //nextdir = dir + 1;
            //    }
            //    else
            //    {
            //        //dir = Options[rnd.Next(0, Options.Count)];
            //    }
            //}
            if (Options.Count > 1)
            {
                Situation mySituation = GetSituationByOptions(Options);
                if (food.posX >= snake.Elements[0].PosX && food.posY >= snake.Elements[0].PosY)
                {
                    if ((food.posX - snake.Elements[0].PosX) > (food.posY - snake.Elements[0].PosY))
                    {
                        if (Options.Contains(0))
                        {
                            dir = 0;
                            logging(dir, 1, Options);
                        }
                        else if (Options.Contains(1))
                        {
                            dir = 1;
                            logging(dir, 2, Options);
                        }
                        else
                        {
                            dir = mySituation.GetBestDir();
                            //dir = Options[rnd.Next(0, Options.Count)];
                            //nextdir = dir;
                            logging(dir, 0, Options);
                        }

                    }
                    else if ((food.posX - snake.Elements[0].PosX) < (food.posY - snake.Elements[0].PosY))
                    {
                        if (Options.Contains(1))
                        {
                            dir = 1;
                            logging(dir, 3, Options);
                        }
                        else if (Options.Contains(0))
                        {
                            dir = 0;
                            logging(dir, 4, Options);
                        }
                        else
                        {
                            dir = mySituation.GetBestDir();
                            //nextdir = dir;
                            //dir = Options[rnd.Next(0, Options.Count)];
                            logging(dir, 0, Options);
                        }
                    }
                    else
                    {
                        //dir = Options[rnd.Next(0, Options.Count)];
                        //nextdir = dir;
                        logging(dir, 0, Options);
                    }
                }
                else if (food.posX >= snake.Elements[0].PosX && food.posY <= snake.Elements[0].PosY)
                {
                    if ((food.posX - snake.Elements[0].PosX) > (snake.Elements[0].PosY - food.posY))
                    {
                        if (Options.Contains(0))
                        {
                            dir = 0;
                            logging(dir, 5, Options);
                        }
                        else if (Options.Contains(3))
                        {
                            dir = 3;
                            logging(dir, 6, Options);
                        }
                        else
                        {
                            dir = mySituation.GetBestDir();
                            //dir = Options[rnd.Next(0, Options.Count)];
                            //nextdir = dir;
                            logging(dir, 0, Options);
                        }

                    }
                    else if ((food.posX - snake.Elements[0].PosX) < (snake.Elements[0].PosY - food.posY))
                    {
                        if (Options.Contains(3))
                        {
                            dir = 3;
                            logging(dir, 7, Options);
                        }
                        else if (Options.Contains(0))
                        {
                            dir = 0;
                            logging(dir, 8, Options);
                        }
                        else
                        {
                            dir = mySituation.GetBestDir();
                            //dir = Options[rnd.Next(0, Options.Count)];
                            //nextdir = dir;
                            logging(dir, 0, Options);
                        }
                    }
                    else
                    {
                        dir = mySituation.GetBestDir();
                        //dir = Options[rnd.Next(0, Options.Count)];
                        //nextdir = dir;
                        logging(dir, 0, Options);
                    }
                }
                else if (food.posX <= snake.Elements[0].PosX && food.posY >= snake.Elements[0].PosY)
                {
                    if ((snake.Elements[0].PosX - food.posX) > (food.posY - snake.Elements[0].PosY))
                    {
                        if (Options.Contains(2))
                        {
                            dir = 2;
                            logging(dir, 9, Options);
                        }
                        else if (Options.Contains(1))
                        {
                            dir = 1;
                            logging(dir, 10, Options);
                        }
                        else
                        {
                            dir = mySituation.GetBestDir();
                            //dir = Options[rnd.Next(0, Options.Count)];
                            //nextdir = dir;
                            logging(dir, 0, Options);
                        }
                    }
                    else if ((snake.Elements[0].PosX - food.posX) < (food.posY - snake.Elements[0].PosY))
                    {
                        if (Options.Contains(1))
                        {
                            dir = 1;
                            logging(dir, 11, Options);
                        }
                        else if (Options.Contains(2))
                        {
                            dir = 2;
                            logging(dir, 12, Options);
                        }
                        else
                        {
                            dir = mySituation.GetBestDir();
                            //dir = Options[rnd.Next(0, Options.Count)];
                            //nextdir = dir;
                            logging(dir, 0, Options);
                        }
                    }
                    else
                    {
                        dir = mySituation.GetBestDir();
                        //dir = Options[rnd.Next(0, Options.Count)];
                        //nextdir = dir;
                        logging(dir, 0, Options);
                    }
                }
                else if (food.posX <= snake.Elements[0].PosX && food.posY <= snake.Elements[0].PosY)
                {
                    if ((snake.Elements[0].PosX - food.posX) > (snake.Elements[0].PosY - food.posY))
                    {
                        if (Options.Contains(2))
                        {
                            dir = 2;
                            logging(dir, 13, Options);
                        }
                        else if (Options.Contains(3))
                        {
                            dir = 3;
                            logging(dir, 14, Options);
                        }
                        else
                        {
                            dir = mySituation.GetBestDir();
                            //dir = Options[rnd.Next(0, Options.Count)];
                            //nextdir = dir;
                            logging(dir, 0, Options);
                        }
                    }
                    else if ((snake.Elements[0].PosX - food.posX) < (snake.Elements[0].PosY - food.posY))
                    {
                        if (Options.Contains(3))
                        {
                            dir = 3;
                            logging(dir, 14, Options);
                        }
                        else if (Options.Contains(2))
                        {
                            dir = 2;
                            logging(dir, 15, Options);
                        }
                        else
                        {
                            dir = mySituation.GetBestDir();
                            //dir = Options[rnd.Next(0, Options.Count)];
                            //nextdir = dir;
                            logging(dir, 0, Options);
                        }
                    }
                    else
                    {
                        dir = mySituation.GetBestDir();
                        //dir = Options[rnd.Next(0, Options.Count)];
                        //nextdir = dir;
                        logging(dir, 0, Options);
                    }
                }
                else
                {
                    dir = mySituation.GetBestDir();
                    logging(dir, 0, Options);
                }
                if (rnd.Next(0, 100) < randomChange)
                {
                    dir = Options[rnd.Next(0, Options.Count)];
                    logging(dir, 999, Options);
                }
            }
            else if(Options.Count == 1)
            {
                dir = Options[0];
            }
            
            

            lastOptions = Options;
            lastdir = dir;
            return dir;
        }
    }
}
