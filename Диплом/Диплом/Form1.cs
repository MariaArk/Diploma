using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Диплом
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        List<System.Drawing.Point> p = new List<System.Drawing.Point> { };
        bool IsClicked = false;
        Rectangle rect = new Rectangle(100, 100, 2,2);
        int deltaX = 0, deltaY = 0, iter = -1;
        Pen pen = new Pen(Color.Black);
        int num = 0;

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                p.Add(new Point(e.X, e.Y));
                pictureBox1.Invalidate();
            }
            if (e.Button == MouseButtons.Right)
            {
                for (int i = 0; i < p.Count; i++)
                {
                    if ((e.X <= p[i].X + 2) && (e.X >= p[i].X))
                        if ((e.Y <= p[i].Y + 2) && (e.Y >= p[i].Y))
                        {
                            IsClicked = true;
                            deltaX = e.X - p[i].X;
                            deltaY = e.Y - p[i].Y;
                            iter = i;
                            break;
                        }
                 }

            }
            
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsClicked)
            {
                rect.X = e.X - deltaX;
                rect.Y = e.Y - deltaY;
                pictureBox1.Invalidate();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string defaultValue = "Введите название файла";
            string value = Interaction.InputBox("Назовите файл", "Сохранение файла", defaultValue);
            if (value != "")
            {
                if (value == "Введите название файла")
                {
                    num += 1;
                    value = "NewFile" + num;
                }
                string filename = "../" + value + ".txt";
                string path = @filename;
                if (!File.Exists(path))
                {
                    // Create a file to write to.
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        for (int i = 0; i < p.Count; i++)
                        {
                            sw.WriteLine(p[i]);
                        }
                    }
                }
                else
                {
                    StreamWriter sw = new StreamWriter(path);
                    for (int i = 0; i < p.Count; i++)
                    {
                        sw.WriteLine(p[i]);
                    }
                    sw.Close();
                }
                MessageBox.Show("файл сохранен");
            } 
                
            
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Pen red = new Pen(Color.Red);
            for (int i = 0; i < p.Count; i++)
            {
                if (iter == i)
                {
                    p[i] = rect.Location;
                    e.Graphics.DrawRectangle(pen, rect);
                }
                else
                    e.Graphics.DrawRectangle(pen, p[i].X, p[i].Y, 2, 2);
            }
            for (int i = 0; i < p.Count-3; i+=4)
            {
                if (p.Count > 3)
                    e.Graphics.DrawBezier(red, p[i], p[i + 1], p[i + 2], p[i + 3]);
            }
        }


        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            IsClicked = false;
        }
    }
}
