using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMPFilter;
using System.IO;
using System.Threading;

namespace VisualFilter2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        String path="", outpath="";

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if(dlg.ShowDialog() == DialogResult.OK)
            {
                path = dlg.FileName;
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                pictureBox1.Image = Image.FromStream(fs);
                fs.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            if (path.Equals("") || outpath.Equals("")) return;
            index = comboBox1.SelectedIndex;
            Thread th = new Thread(new ThreadStart(backgroundTask));
            th.Start();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                outpath = dlg.FileName;
            }
        }
        int index;
        void backgroundTask()
        {
            BitmapIO io = new BitmapIO();
            io.ReadBmp(path);
            Filters filters = new Filters(io);
            progressBar1.Invoke((MethodInvoker)delegate
            {
                progressBar1.Maximum = (io.GetInfo().biHeight - 2) * (io.GetInfo().biWidth - 2);
            });
            filters.ApplyFilter(index, progressBar1);
            io.WriteBmp(outpath);
            FileStream fs = new FileStream(outpath, FileMode.Open, FileAccess.Read);
            pictureBox1.Invoke((MethodInvoker)delegate
            {
                pictureBox1.Image = Image.FromStream(fs);
            });
            fs.Close();
        }


    }
}
