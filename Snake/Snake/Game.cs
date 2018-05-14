using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Snake.DL;

namespace Snake
{
    public partial class Game : Form
    {
        private DL.Snake currentSnake { get; set; }
        private Food currendFood { get; set; }
        private bool aiMode = true;
        public static bool alive = true;
        public int best = 0;
        int count = 0;
        int sum = 0;
        public Game()
        {
            currentSnake = new DL.Snake();
            PlayerInit();
            InitializeComponent();
        }

        public void PlayerInit()
        {
            Player.Situations = new List<Situation>();
            List<string> files = new List<string> { "0", "01", "012", "013", "02", "023", "03", "1", "12", "123", "13", "2", "23", "3" };
            foreach (string file in files)
            {
                string path = file + ".csv";
                if (!File.Exists(path))                
                {
                    string tmp = "";
                    foreach(char c in file)
                    {
                        tmp = tmp + c + ";0" + Environment.NewLine;
                    }
                    File.WriteAllText(path, tmp);
                }
                List<Tuple<int, int>> Options = new List<Tuple<int, int>>();
                StreamReader reader = new StreamReader(File.OpenRead(path));
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (!String.IsNullOrWhiteSpace(line))
                    {
                        string[] values = line.Split(';');
                        Options.Add(new Tuple<int, int>(Convert.ToInt32(values[0]), Convert.ToInt32(values[1])));
                    }
                }
                reader.Close();
                Player.Situations.Add(new Situation(Options));
            }
        }
        public void WriteSituations()
        {
            foreach(Situation sit in Player.Situations)
            {
                string path = "";
                string text = "";
                foreach (Tuple<int,int> o in sit.Options)
                {
                    path += o.Item1.ToString();
                    text += o.Item1.ToString() + ";" + o.Item2.ToString()+Environment.NewLine;
                }
                path = path+".csv";
                
                File.WriteAllText(path, text);
            }
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            if (timer1.Enabled && !aiMode)
            {
                if (e.KeyCode == Keys.Up)
                {
                    currentSnake.dir = 3;
                }
                if (e.KeyCode == Keys.Down)
                {
                    currentSnake.dir = 1;
                }
                if (e.KeyCode == Keys.Left)
                {
                    currentSnake.dir = 2;
                }
                if (e.KeyCode == Keys.Right)
                {
                    currentSnake.dir = 0;
                }
            }
            if(e.KeyCode == Keys.Space)
            {
                if (timer1.Enabled)
                {
                    timer1.Stop();
                }
                else if(alive)
                {
                    timer1.Start();
                }
            }
            if (e.KeyCode == Keys.G)
            {
                aiMode = !aiMode;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (alive)
            {
                currentSnake.Shift();
                draw(null);
                checkFood();
                checkDeath();
                if (currentSnake.level > best)
                {
                    best = currentSnake.level;
                }
            }
            if (aiMode)
            {
                currentSnake.dir = Player.ChooseDir(currentSnake, currendFood);
            }
            else
            {
                Player.lastdir = currentSnake.dir;
            }
        }

        private void draw(PaintEventArgs e)
        {
            Graphics g = this.CreateGraphics();
            g.Clear(Color.Black);

            g.FillEllipse(new SolidBrush(Color.Red), new Rectangle(currendFood.posX, currendFood.posY, currendFood.width, currendFood.width));

            foreach (SnakeDot dot in currentSnake.Elements)
            {
                Brush brush = new SolidBrush(dot.dotColor);

                g.FillEllipse(brush,dot.PosX, dot.PosY, dot.Width, dot.Width);
                //g.DrawEllipse(new Pen(currentSnake.GetSnakeColor()),new Rectangle(dot.PosX,dot.PosY,dot.Width,dot.Width));
            }
            int avg = count>0?(int)(sum / count):0;
            this.Text = "Snake - Level #" + currentSnake.level+" - Best: #"+best+" - AVG: #"+avg+ " - Round: "+(count + 1);
        }

        private void generateFood()
        {
            Random rnd = new Random();

            int maxX = this.Width / (Globals.Width +1);
            int maxY = this.Height / (Globals.Width +1);


            int x = rnd.Next(10, maxX-10);
            int y = rnd.Next(10, maxY-10);

            x = x * (Globals.Width + 1) + 1;
            y = y * (Globals.Width + 1) - 1;
            currendFood = new Food(x,y);
        }

        private void checkFood()
        {
            if(currentSnake.Elements[0].PosX == currendFood.posX && currentSnake.Elements[0].PosY == currendFood.posY)
            {
                currentSnake.level++;
                currentSnake.Append();
                generateFood();
            }
        }
        private void checkDeath()
        {
             bool stillAlive = true;
            for (int i = 1; i < currentSnake.Elements.Count; i++)
            {
                if (currentSnake.Elements[0].PosX == currentSnake.Elements[i].PosX && currentSnake.Elements[0].PosY == currentSnake.Elements[i].PosY)
                {
                    stillAlive = false;
                }

            }

            if(currentSnake.Elements[0].PosX <= 0 || currentSnake.Elements[0].PosY <= 0 || (currentSnake.Elements[0].PosY + Globals.Width + 1) >= this.Height || (currentSnake.Elements[0].PosX + Globals.Width + 1) >= this.Width)
            {
                stillAlive = false;
            }


            if (!stillAlive)
            {
                alive = false;
                if (aiMode)
                {
                    Player.DownVoteLast();
                    WriteSituations();
                }
                timer1.Stop();
                var tmp = Player.Log;
                Graphics g = this.CreateGraphics();
                g.Clear(Color.Black);

                g.FillEllipse(new SolidBrush(Color.Red), new Rectangle(currendFood.posX, currendFood.posY, currendFood.width, currendFood.width));

                foreach (SnakeDot dot in currentSnake.Elements)
                {
                    g.FillEllipse(new SolidBrush(Color.Red), new Rectangle(dot.PosX, dot.PosY, dot.Width, dot.Width));
                }

                restart();
            }
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            currentSnake.Elements.Add(new SnakeDot((Globals.Width * 10), 10,Color.Green));
            currentSnake.Append();
            currentSnake.Append();
            currentSnake.Append();

            generateFood();

            timer1.Start();
            btnStart.Enabled = false;
            btnStart.Visible = false;

            //currentSnake.dir = 2;
        }
        
        private void restart()
        {
            sum += currentSnake.level;
            count++;
            currentSnake = currentSnake = new DL.Snake();
            generateFood();
            Player.Log = new List<Tuple<int, int, List<int>>>();


            currentSnake.Elements.Add(new SnakeDot((Globals.Width * 10), 10, Color.Green));
            currentSnake.Append();
            currentSnake.Append();
            currentSnake.Append();

            alive = true;
            timer1.Start();
        }

        private void Game_Load(object sender, EventArgs e)
        {

        }
    }
}
