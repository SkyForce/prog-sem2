using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace BMPFilter
{
    

    class Program
    {

        static void Main(string[] args)
        {
            int type = Int16.Parse(args[2]);
	        if (type < 0 || type > 6)
	        {
		        Console.WriteLine("Incorrect filter!");
	        }



            BitmapIO io = new BitmapIO();
            io.ReadBmp(args[0]);

            Filters filters = new Filters(io);
            filters.ApplyFilter(type);

            io.WriteBmp(args[1]);



	        Console.WriteLine("Done!");
        }
    }
}
