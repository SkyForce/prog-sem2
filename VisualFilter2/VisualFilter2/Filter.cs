using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisualFilter2;

namespace BMPFilter
{
    class Filter
    {
        int width, height;
        byte[,,] bmp;
        int[, ,] newbmp;
        protected double[,] coeff;

        public Filter(byte[,,] _bmp, int[,,] _newbmp, int w, int h)
        {
            newbmp = _newbmp;
            bmp = _bmp;
            coeff = new double[3, 3];
            width = w;
            height = h;
        }

        int perform(byte[, ,] bmp, int x, int y, double[,] coeff, int k)
        {
            double sum = 0;
            int i, j;
            for (i = x - 1; i <= x + 1; i++)
            {
                for (j = y - 1; j <= y + 1; j++)
                {
                    sum += bmp[i, j, k] * coeff[i - x + 1, j - y + 1];
                }
            }
            return (int)sum;
        }

        public void Apply(ProgressBar pb)
        {


		    for (int i = 1; i < height - 1; i++)
		    {
			    for (int j = 1; j < width - 1; j++)
			    {
                    newbmp[i, j, 0] = (perform(bmp, i, j, coeff, 0));
                    newbmp[i, j, 1] = (perform(bmp, i, j, coeff, 1));
                    newbmp[i, j, 2] = (perform(bmp, i, j, coeff, 2));
                    pb.Invoke((MethodInvoker)delegate
                    {
                        //Code to modify control will go here
                        pb.Increment(1);
                    });
                }
            }
        }

        delegate void IncrementProgressCallback(ProgressBar pb);
        void IncrementProgress(ProgressBar progressBar1)
        {
            if (progressBar1.InvokeRequired)
            {
                IncrementProgressCallback c = new IncrementProgressCallback(IncrementProgress);
                Form1.ActiveForm.Invoke(c);
            }
            else
            {
                progressBar1.Increment(1);
            }
        }

    }
}
