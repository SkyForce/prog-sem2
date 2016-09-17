using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMPFilter
{
    class Filters
    {
        BitmapIO io;
        byte[, ,] bmp;
        int[, ,] newbmp;
        BitmapHeader h;
        BitmapInfo inf;

        public Filters(BitmapIO io)
        {
            this.io = io;
            bmp = io.GetBmp();
            newbmp = io.GetNewBmp();
            h = io.GetHeader();
            inf = io.GetInfo();
        }


        public void ApplyFilter(int type)
        {
            Filter filter;
            switch (type)
            {
                case 0:
                    filter = new AverageFilter(bmp, newbmp, inf.biWidth, inf.biHeight);
                    break;
                case 1:
                    filter = new SobelXFilter(bmp,  newbmp, inf.biWidth, inf.biHeight);
                    break;
                case 2:
                    filter = new SobelYFilter(bmp,  newbmp, inf.biWidth, inf.biHeight);
                    break;
                case 3:
                    filter = new GaussXFilter(bmp,  newbmp, inf.biWidth, inf.biHeight);
                    break;
                case 4:
                    filter = new GaussYFilter(bmp,  newbmp, inf.biWidth, inf.biHeight);
                    break;
                case 5:
                    filter = new SharpenFilter(bmp,  newbmp, inf.biWidth, inf.biHeight);
                    break;
                default:
                    filter = new BlurFilter(bmp,  newbmp, inf.biWidth, inf.biHeight);
                    break;

                
            }
            filter.Apply();
        }
    }
}
