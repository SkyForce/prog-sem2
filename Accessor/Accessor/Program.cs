using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Accessor
{
    class Program
    {
        static void Main(string[] args)
        {
            XX X = new XX();
            Accessor acc = new Accessor();
            Console.WriteLine(acc.GetDelegate<XX>("X.Y.Z.T")(X));
            Console.ReadKey();
        }
    }
}
