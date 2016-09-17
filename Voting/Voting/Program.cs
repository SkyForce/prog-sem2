using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Voting
{
    class Program
    {
        static void Main(string[] args)
        {
            int n, m;
            n = int.Parse(Console.ReadLine());
            m = int.Parse(Console.ReadLine());
            Voting voting = new Voting(n, m);
            Console.WriteLine("Candidate {0} won", voting.PerformVoting());
        }
    }
}
