using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Configuration;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace EmailApp
{
    public partial class Form1 : Form
    {
        Random plats = new Random();
        List<Point> poang = new List<Point>();
        private int point;
        private int count;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LabelValues();
            Timers();
            PictureLocations();
            RandomFruitLocation();
        }

        private void LabelValues()
        {
            label3.Text = "0";
            label1.Text = "0";

            count = 1;
            point = 1;
        }

        private void PictureLocations()
        {
            pictureBox1.Location = new Point(5, 2);
            pictureBox3.Location = new Point(0, 100);
            pictureBox4.Location = new Point(0, 250);

            for (var i = 0; i <= 100; i++)
            {
                poang.Add(new Point(plats.Next(500), plats.Next(270)));
            }
        }

        private void Timers()
        {
            timer1.Interval = 1000;
            timer1.Start();

            timer2.Interval = 15;
            timer2.Start();

            timer3.Interval = 100;
            timer3.Start();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    pictureBox1.Top -= 5;
                    break;
                case Keys.Down:
                    pictureBox1.Top += 5;
                    break;
                case Keys.Left:
                    pictureBox1.Left -= 5;
                    break;
                case Keys.Right:
                    pictureBox1.Left += 5;
                    break;
            }

            if (FruitTouch)
            {
                label3.Text = point++.ToString();
                RandomFruitLocation();
            }

            if (OutOfBounds)
            {
                SystemSounds.Beep.Play();
                Form1_Load(sender, e);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = count++.ToString();
        }

        public void RandomFruitLocation()
        {
            pictureBox2.Location = poang[plats.Next(100)];
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            var picture3 = pictureBox3.Location;
            var picture4 = pictureBox4.Location;
           
            if (pictureBox3.Right >= panel1.Right)
            {
                picture3.X = 0;
                picture4.X = 0;
            }
            else
            {
                picture3.X += 2;
                picture4.X += 2;
            }

            pictureBox3.Location = picture3;
            pictureBox4.Location = picture4;
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (pictureBox1.Bounds.IntersectsWith(pictureBox3.Bounds)
                || pictureBox1.Bounds.IntersectsWith(pictureBox4.Bounds))
            {
                SystemSounds.Beep.Play();
                Form1_Load(sender, e);
            }
        }
        public bool FruitTouch
        {
            get { return pictureBox1.Bounds.IntersectsWith(pictureBox2.Bounds); }
        }

        public bool OutOfBounds
        {
            get
            {
                return pictureBox1.Location.Y <= 0
                       || pictureBox1.Location.X <= 0
                       || pictureBox1.Right >= panel1.Right
                       || pictureBox1.Bottom >= panel1.Bottom;
            }
        }
    }
}
