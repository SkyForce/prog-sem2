using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack1
{
    class Player
    {
        public int playerCount = 0;
        public Cards.Card myCard1 = 0, myCard2 = 0;
        public void CheckScoreWithTUZ()
        {
            if ((myCard1 == Cards.Card.TUZ || myCard2 == Cards.Card.TUZ) && playerCount > 21) playerCount -= 10;
        }

    }
}
