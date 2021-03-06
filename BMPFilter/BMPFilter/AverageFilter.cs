﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMPFilter
{
    class AverageFilter : Filter
    {
        public AverageFilter(byte[, ,] _bmp, int[, ,] _newbmp, int w, int h) : base(_bmp, _newbmp, w, h)
        {
            coeff[0, 0] = 1.0 / 9;
            coeff[0, 1] = 1.0 / 9;
            coeff[0, 2] = 1.0 / 9;
            coeff[1, 0] = 1.0 / 9;
            coeff[1, 1] = 1.0 / 9;
            coeff[1, 2] = 1.0 / 9;
            coeff[2, 0] = 1.0 / 9;
            coeff[2, 1] = 1.0 / 9;
            coeff[2, 2] = 1.0 / 9;

        }
    }
}
