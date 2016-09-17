using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMPFilter
{
    class GaussYFilter : Filter
    {
        public GaussYFilter(byte[, ,] _bmp, int[, ,] _newbmp, int w, int h)
            : base(_bmp, _newbmp, w, h)
        {
            coeff[0,0] = -1;
            coeff[0,1] = -0.980199;
            coeff[0,2] = -0.923116;
            coeff[1,0] = 0;
            coeff[1,1] = 0;
            coeff[1,2] = 0;
            coeff[2,0] = 0.923116;
            coeff[2,1] = 0.904837;
            coeff[2,2] = 0.852144;
        }
    }
}
