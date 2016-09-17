using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMPFilter
{
    class BlurFilter : Filter
    {
        public BlurFilter(byte[, ,] _bmp, int[, ,] _newbmp, int w, int h)
            : base(_bmp, _newbmp, w, h)
        {
            coeff[0,0] = 0.0625;
            coeff[0,1] = 0.125;
            coeff[0,2] = 0.0625;
            coeff[1,0] = 0.125;
            coeff[1,1] = 0.25;
            coeff[1,2] = 0.125;
            coeff[2,0] = 0.0625;
            coeff[2,1] = 0.125;
            coeff[2,2] = 0.0625;
        }
    }
}
