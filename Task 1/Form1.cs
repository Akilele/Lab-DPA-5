using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task_1
{
    public partial class Form1 : Form
    {
        Point p1, p2;
        bool _click = false;
        Graphics g;
        Pen pen = new Pen(Color.Black, 2);

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.MouseClick += OnPictureBoxClicked;
        }

        private void OnPictureBoxClicked(object sender, MouseEventArgs e)
        {
            if (_click == false)
            {
                _click = true;
                p1 = e.Location;
                pictureBox1.MouseClick -= OnPictureBoxClicked;
                label1.Text = p1.ToString();
            }
            else
            {
                _click = false;
                p2 = e.Location;
                pictureBox1.MouseClick -= OnPictureBoxClicked;
                label2.Text = p2.ToString();
                Drawing();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }

        private void Drawing()
        {
            g = Graphics.FromHwnd(pictureBox1.Handle);
            if(radioButton1.Checked)
            {
                g.DrawLine(pen, p1, p2);
            }
            else if(radioButton2.Checked)
            {
                g.DrawLine(pen, p1.X, p1.Y, p2.X, p1.Y);
                g.DrawLine(pen, p1.X, p2.Y, p2.X, p2.Y);
                g.DrawLine(pen, p1.X, p1.Y, p1.X, p2.Y);
                g.DrawLine(pen, p2.X, p1.Y, p2.X, p2.Y);
            }
            else if(radioButton3.Checked)
            {
                int R = Math.Abs(p2.X - p1.X);
                int X1 = p1.X;
                int Y1 = p1.Y;
                int x = 0;
                int y = R;
                int delta = 1 - 2 * R;
                int error = 0;
                while (y >= 0)
                {
                    drawpixel(g, X1 + x, Y1 + y);
                    drawpixel(g, X1 + x, Y1 - y);
                    drawpixel(g, X1 - x, Y1 + y);
                    drawpixel(g, X1 - x, Y1 - y);
                    error = 2 * (delta + y) - 1;
                    if((delta < 0) && (error <= 0))
                    {
                        delta += 2 * ++x + 1;
                        continue;
                    }
                    if ((delta > 0) && (error > 0))
                    {
                        delta -= 2 * --y + 1;
                        continue;
                    }
                    delta += 2 * (++x - y--);
                }
            }
        }

        private void drawpixel(Graphics g, int v1, int v2)
        {
            g.FillRectangle(pen.Brush, v1, v2, 1, 1);
        }
    }
}
