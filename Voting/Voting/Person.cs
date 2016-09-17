using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Voting
{
    class Person
    {
        int candidates;
        int[] chances;
        public Person(int cands)
        {
            candidates = cands;
            chances = new int[candidates];
            GenerateChances();
        }

        void GenerateChances()
        {
            int pos = 0;
            for (int i = 1; i < candidates; i++)
            {
                int lpos = pos;
                pos = RandomChoice.rand.Next(pos, 10000);
                chances[i - 1] = pos - lpos;
            }
            chances[candidates - 1] = 10000 - pos;
            chances = chances.OrderBy(x => RandomChoice.rand.Next()).ToArray();
        }

        public int Vote()
        {
            int val = RandomChoice.rand.Next(10000);
            int sum = 0;
            for (int i = 0; i < candidates; i++)
            {
                sum += chances[i];
                if (sum >= val)
                    return i;
            }
            return -1;
        }

        public int SecondVote(int i, int j)
        {
            int sum = chances[i] + chances[j];
            int val = RandomChoice.rand.Next(sum);
            if (chances[i] >= val) return i;
            else return j;
        }

    }
}
