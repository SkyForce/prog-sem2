using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Voting
{
    class Voting
    {
        int candidates, voters;
        ArrayList persons;
        int[] results;

        public Voting(int n, int m)
        {
            candidates = n;
            voters = m;
            persons = new ArrayList();
            results = new int[candidates];
            InitVoters();
        }

        void InitVoters()
        {
            for (int i = 0; i < voters; i++)
            {
                persons.Add(new Person(candidates));
            }
        }

        public int PerformVoting()
        {
            for (int i = 0; i < voters; i++)
            {
                results[((Person)persons[i]).Vote()]++;
            }
            int mx = 0, mn = 0, imx = 0, imn = 0;
            for (int i = 0; i < candidates; i++)
            {
                if (results[i] >= voters / 2) return i + 1;
                if (results[i] > mx)
                {
                    mn = mx;
                    imn = imx;
                    mx = results[i];
                    imx = i;
                }
                else if (results[i] > mn)
                {
                    imn = i;
                    mn = results[i];
                }
            }
            results[imx] = results[imn] = 0;
            for (int i = 0; i < voters; i++)
            {
                results[((Person)persons[i]).SecondVote(imn, imx)]++;
            }
            return (results[imx] > results[imn]) ? imx + 1 : imn + 1;
        }
    }
}
