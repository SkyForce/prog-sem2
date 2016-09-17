using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graphic
{
    static class Functions
    {
        public static double sin(double arg)
        {
            return Math.Sin(arg);
        }

        public static double cos(double arg)
        {
            return Math.Cos(arg);
        }

        public static double exp(double arg)
        {
            return Math.Exp(arg);
        }

        public static double x3(double arg)
        {
            return arg * arg * arg;
        }

        public static double sqrt(double arg)
        {
            if(arg > 0) return Math.Sqrt(arg);
            return 0;
        }

    }
}
