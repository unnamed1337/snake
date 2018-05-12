using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        public Game()
        {
            currentSnake = new DL.Snake();
            InitializeComponent();
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Up)
            {
                currentSnake.dir = 3;
            }
            if(e.KeyCode == Keys.Down)
            {
                currentSnake.dir = 1;
            }
            if(e.KeyCode == Keys.Left)
            {
                currentSnake.dir = 2;
            }
            if(e.KeyCode== Keys.Right)
            {
                currentSnake.dir = 0;
            }
            if(e.KeyCode == Keys.Space)
            {
                if (timer1.Enabled)
                {
                    timer1.Stop();
                }
                else
                {
                    timer1.Start();
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            currentSnake.Shift();
            draw(null);
            checkFood();
            checkDeath();
        }

        private void draw(PaintEventArgs e)
        {
            Graphics g = this.CreateGraphics();
            g.Clear(Color.Black);

            g.FillEllipse(new SolidBrush(Color.Red), new Rectangle(currendFood.posX, currendFood.posY, currendFood.width, currendFood.width));

            foreach (SnakeDot dot in currentSnake.Elements)
            {
                Brush brush = new SolidBrush(currentSnake.GetSnakeColor());

                g.FillEllipse(brush,dot.PosX, dot.PosY, dot.Width, dot.Width);
                //g.DrawEllipse(new Pen(currentSnake.GetSnakeColor()),new Rectangle(dot.PosX,dot.PosY,dot.Width,dot.Width));
            }
            this.Text = "Snake - Level #" + currentSnake.level;
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
                timer1.Stop();
                Graphics g = this.CreateGraphics();
                g.Clear(Color.Black);

                g.FillEllipse(new SolidBrush(Color.Red), new Rectangle(currendFood.posX, currendFood.posY, currendFood.width, currendFood.width));

                foreach (SnakeDot dot in currentSnake.Elements)
                {
                    g.FillEllipse(new SolidBrush(Color.Red), new Rectangle(dot.PosX, dot.PosY, dot.Width, dot.Width));
                }
            }
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            currentSnake.Elements.Add(new SnakeDot((Globals.Width * 10), 10));
            currentSnake.Append();
            currentSnake.Append();

            generateFood();

            timer1.Start();
            btnStart.Enabled = false;
            btnStart.Visible = false;

            //currentSnake.dir = 2;
        }

        private void Game_Load(object sender, EventArgs e)
        {

        }
    }
}
