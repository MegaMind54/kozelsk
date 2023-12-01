using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Slavianskaya_zmeika
{
    public partial class Form1 : Form
    {
        private int rI, rJ;
        private readonly PictureBox banan;
        private readonly PictureBox[] zmei = new PictureBox[400];
        private readonly Label labelScore;
        private int dirX, dirY;
        private readonly int width = 900;
        private readonly int height = 800;
        private readonly int side = 40;
        private int score = 0;
        public Form1()
        {
            InitializeComponent();
            this.Text = "Slavianskaya zmeika";
            this.Width = width;
            this.Height = height;
            dirX = 1;
            dirY = 0;
            labelScore = new Label
            {
                Text = "Score: 0",
                Location = new Point(810, 10)
            };
            this.Controls.Add(labelScore);
            zmei[0] = new PictureBox
            {
                Location = new Point(201, 201),
                Size = new Size(side - 1, side - 1),
                BackColor = Color.Red
            };
            this.Controls.Add(zmei[0]);
            banan = new PictureBox
            {
                BackColor = Color.Yellow,
                Size = new Size(side, side)
            };
            Map();
            Banans();
            timer.Tick += new EventHandler(Update);
            timer.Interval = 50;
            timer.Start();
            this.KeyDown += new KeyEventHandler(Movement);
        }

        private void Banans()
        {
            Random r = new Random();
            rI = r.Next(0, height - side);
            int tempI = rI % side;
            rI -= tempI;
            rJ = r.Next(0, height - side);
            int tempJ = rJ % side;
            rJ -= tempJ;
            rI++;
            rJ++;
            banan.Location = new Point(rI, rJ);
            this.Controls.Add(banan);
        }
        private void CheckBorders()
        {
            if (zmei[0].Location.X <= 0)
            {
                for (int _i = 1; _i <= score; _i++)
                {
                    this.Controls.Remove(zmei[_i]);
                }
                score = 0;
                labelScore.Text = "Score: " + score;
                dirX = 1;
            }
            if (zmei[0].Location.X >= height)
            {
                for (int _i = 1; _i <= score; _i++)
                {
                    this.Controls.Remove(zmei[_i]);
                }
                score = 0;
                labelScore.Text = "Score: " + score;
                dirX = -1;
            }
            if (zmei[0].Location.Y <= 0)
            {
                for (int _i = 1; _i <= score; _i++)
                {
                    this.Controls.Remove(zmei[_i]);
                }
                score = 0;
                labelScore.Text = "Score: " + score;
                dirY = 1;
            }
            if (zmei[0].Location.Y >= height)
            {
                for (int _i = 1; _i <= score; _i++)
                {
                    this.Controls.Remove(zmei[_i]);
                }
                score = 0;
                labelScore.Text = "Score: " + score;
                dirY = -1;
            }
        }
        private void EatItself()
        {
            for (int _i = 1; _i < score; _i++)
            {
                if (zmei[0].Location == zmei[_i].Location)
                {
                    for (int _j = _i; _j <= score; _j++)
                        this.Controls.Remove(zmei[_j]);
                    score -= (score - _i + 1);
                    labelScore.Text = "Score: " + score;
                }
            }
        }
        private void Eating()
        {
            if (zmei[0].Location.X == rI && zmei[0].Location.Y == rJ)
            {
                labelScore.Text = "Score: " + ++score;
                zmei[score] = new PictureBox
                {
                    Location = new Point(zmei[score - 1].Location.X + 40 * dirX, zmei[score - 1].Location.Y - 40 * dirY),
                    Size = new Size(side - 1, side - 1),
                    BackColor = Color.Red
                };
                this.Controls.Add(zmei[score]);
                Banans();
            }
        }

        private void Map()
        {
            for (int i = 0; i <= width / side; i++)
            {
                PictureBox pic = new PictureBox
                {
                    BackColor = Color.Black,
                    Location = new Point(0, side * i),
                    Size = new Size(width - 100, 1)
                };
                this.Controls.Add(pic);
            }
            for (int i = 0; i <= height / side; i++)
            {
                PictureBox pic = new PictureBox
                {
                    BackColor = Color.Black,
                    Location = new Point(side * i, 0),
                    Size = new Size(1, width)
                };
                this.Controls.Add(pic);
            }
        }
        private void Moving()
        {
            for (int i = score; i >= 1; i--)
            {
                zmei[i].Location = zmei[i - 1].Location;
            }
            zmei[0].Location = new Point(zmei[0].Location.X + dirX * (side), zmei[0].Location.Y + dirY * (side));
            EatItself();
        }
        private void Update(object myObject, EventArgs eventsArgs)
        {
            CheckBorders();
            Eating();
            Moving();
        }
        private void Movement(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode.ToString())
            {
                case "Right":
                    dirX = 1;
                    dirY = 0;
                    break;
                case "Left":
                    dirX = -1;
                    dirY = 0;
                    break;
                case "Up":
                    dirY = -1;
                    dirX = 0;
                    break;
                case "Down":
                    dirY = 1;
                    dirX = 0;
                    break;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
