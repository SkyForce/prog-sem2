using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Graphic
{
    class Drawer
    {
        public delegate double FuncDelegate(double args);

        Graphics g;
        PictureBox pbx;
        int width, height;

        public Drawer(PictureBox _pbx) 
        {
            pbx = _pbx;

            g = pbx.CreateGraphics();
        }

        public void DrawSystem()
        {
            g.Clear(Color.White);

            g.DrawLine(new Pen(Color.Black), 0, pbx.Height / 2, pbx.Width, pbx.Height / 2);
            g.DrawLine(new Pen(Color.Black), pbx.Width / 2, 0, pbx.Width / 2, pbx.Height);

            width = pbx.Width;
            height = pbx.Height;

            int n = 10;
            for (int i = width / 2; i < width; i += width / n)
            {
                g.DrawLine(new Pen(Color.Black), i, height / 2 - 10, i, height / 2 + 10);
                g.DrawLine(new Pen(Color.Black), width - i, height / 2 - 10, width - i, height / 2 + 10);
            }
            for (int i = height / 2 + height / n; i < height; i += height / n)
            {
                g.DrawLine(new Pen(Color.Black), width / 2 - 10, i, width / 2 + 10, i);
                g.DrawLine(new Pen(Color.Black), width / 2 - 10, height - i, width / 2 + 10, height - i);
            }
        }

        public void Draw(FuncDelegate del)
        {
            width = pbx.Width;
            height = pbx.Height;

            int n = 10;

            double lastX = -5, lastY = del(lastX);
            double coeffx = width / n, coeffy = height / n;
            double x, y;

            for (int i = 1; i < width; i++)
            {
                x = -n / 2 + i / coeffx;
                y = del(x);
                g.DrawLine(new Pen(Color.Red), (float)(lastX * coeffx + n * coeffx / 2), (float)(height / 2 - lastY * coeffy), (float)(x * coeffx + n * coeffx / 2), (float)(height / 2 - y * coeffy));
                lastX = x;
                lastY = y;
            }
        }
    }
}
