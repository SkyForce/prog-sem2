using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMPFilter
{
    class SharpenFilter : Filter
    {
        public SharpenFilter(byte[, ,] _bmp, int[, ,] _newbmp, int w, int h)
            : base(_bmp, _newbmp, w, h)
        {
            for (int i = 0; i < 3; i++)
            {
                coeff[0,i] = coeff[i,0] = coeff[2,i] = coeff[i,2] = -1;
            }
            coeff[1,1] = 9;
        }
    }
}
