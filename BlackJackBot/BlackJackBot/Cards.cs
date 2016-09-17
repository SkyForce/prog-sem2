using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BlackJack1
{
    class Cards
    {
        public delegate void ShowCardHandler(Card card);
        public event ShowCardHandler OnOpen;

        List<Card> cards;
        public enum Card
        {
            VALET, DAMA, KING,
            TUZ,
            TWO, THREE, FOUR, FIVE, SIX, SEVEN, EIGHT, NINE, TEN
        };

        public int scoreCount;

        public Cards()
        {
            RefreshCards();
        }

        void Shuffle(IList list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Card value = (Card)list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        void RefreshCards()
        {
            scoreCount = 0;
            cards = new List<Card>();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        cards.Add((Card)j);
                    }
                }
            }
            Shuffle(cards);
        }

        public int getCard(ref Card c)
        {
            if (cards.Count == 52)
            {
                Console.WriteLine("Refreshing cards");
                RefreshCards();
            }
            Card card = cards.First();
            int score;
            switch (card)
            {
                case Card.DAMA:
                case Card.VALET:
                case Card.KING:
                    score = 10; break;
                case Card.TUZ:
                    score = 11; break;
                case Card.TEN:
                    score = (int)card - 2; break;
                case Card.SEVEN:
                case Card.EIGHT:
                case Card.NINE:
                    score = (int)card - 2; break;
                default: score = (int)card - 2; break;
            }
            c = card;
            cards.Remove(card);

            OnOpen(card);

            return score;
        }

        public int getCount()
        {
            return cards.Count;
        }
    }
}
