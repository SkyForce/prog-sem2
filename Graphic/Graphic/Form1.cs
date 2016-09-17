using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Graphic
{
    public partial class Form1 : Form
    {
        Drawer dr = null;
        public Form1()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dr == null) dr = new Drawer(pictureBox1);
            dr.DrawSystem();
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    dr.Draw(Functions.sin);
                    break;
                case 1:
                    dr.Draw(Functions.cos);
                    break;
                case 2:
                    dr.Draw(Functions.exp);
                    break;
                case 3:
                    dr.Draw(Functions.x3);
                    break;
                case 4:
                    dr.Draw(Functions.sqrt);
                    break;
            }
        }
    }
}
