using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack1
{
    class Player
    {
        int scoreCount;
        Cards cards;
        public Player(Cards c)
        {
            cards = c;
            scoreCount = 0;
            c.OnOpen += new Cards.ShowCardHandler(c_OnOpen);
        }


        void c_OnOpen(Cards.Card card)
        {
            switch (card)
            {
                case Cards.Card.DAMA:
                case Cards.Card.VALET:
                case Cards.Card.KING:
                case Cards.Card.TUZ:
                case Cards.Card.TEN:
                    scoreCount--;
                    break;
                case Cards.Card.SEVEN:
                case Cards.Card.EIGHT:
                case Cards.Card.NINE:
                    break;
                default: scoreCount--; break;
            }
        }

        public int playerCount = 0;
        public Cards.Card myCard1 = 0, myCard2 = 0;
        public void CheckScoreWithTUZ()
        {
            if ((myCard1 == Cards.Card.TUZ || myCard2 == Cards.Card.TUZ) && playerCount > 21) playerCount -= 10;
        }

        public void SetRate(ref double rate, double cash)
        {
            double real = scoreCount / (4 - cards.getCount() % 52);
            rate += 200 * real;
            if (rate < 0) rate = 0;
            else if (rate > cash) rate = cash;
            else if (real < -1) rate = 0;
        }

        public bool IsHit()
        {
            if (playerCount < 17 && playerCount >= -1 && scoreCount >= -1) return true;
            else return false;
        }
    }
}
