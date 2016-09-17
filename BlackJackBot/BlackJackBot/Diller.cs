using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack1
{
    class Diller
    {
        public int dillerCount = 0;
        public Cards.Card dillerCard = 0, dillerCard2 = 0;
        public void CheckScoreWithTUZ()
        {
            if ((dillerCard == Cards.Card.TUZ) && dillerCount > 21) dillerCount -= 10;
        }
        
    }
}
