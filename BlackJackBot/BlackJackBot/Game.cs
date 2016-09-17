using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BlackJack1
{
    class Game
    {
        double rate = 0, cash = 1000;
        Cards cards;

        public Game(int money)
        {
            cash = money;
            rate = cash / 5;
            cards = new Cards();
        }

        bool AskForExit()
        {
            Console.WriteLine("Do you want play? (y/n): ");
            if (Console.ReadLine() == "y")
                return false;
            else return true;
        }

        public void Play()
        {
            Cards c = new Cards();
            Player player = new Player(c);
            

            while(true)
            {
                player.playerCount = 0;
                player.myCard1 = player.myCard2 = 0;
                Diller diller = new Diller();

                player.SetRate(ref rate, cash);
                Console.WriteLine("rate {0}", rate);

                player.playerCount += c.getCard(ref player.myCard1);
                player.playerCount += c.getCard(ref player.myCard2);

                diller.dillerCount += c.getCard(ref diller.dillerCard);
                diller.dillerCount += c.getCard(ref diller.dillerCard2);

                diller.CheckScoreWithTUZ();
                player.CheckScoreWithTUZ();
                
                Console.WriteLine("Your cards is {0}, {1}; Diller's card is {2}", player.myCard1, player.myCard2, diller.dillerCard);


                if (player.playerCount == 21)
                {
                    Console.WriteLine("BlackJack!");
                    if (diller.dillerCard != Cards.Card.TUZ)
                    {
                        cash += rate * 1.5;
                        Console.WriteLine("You won {0}, your cash: {1}", rate * 1.5, cash);
                        if (AskForExit()) break;
                    }
                }
                else if (diller.dillerCount == 21)
                {
                    cash -= rate;
                    Console.WriteLine("Diller got BlackJack! You lose {0}, your cash: {1}", rate, cash);
                    if (AskForExit()) break;
                }

                while (true)
                {
                    if (player.IsHit())
                    {
                        player.playerCount += c.getCard(ref player.myCard1);
                        player.CheckScoreWithTUZ();
                        Console.WriteLine("Card is {0}, current score: {1}", player.myCard1, player.playerCount);
                    }
                    else break;
                }

                if (diller.dillerCount >= 17) Console.WriteLine("Diller's count: {0}", diller.dillerCount);

                while (diller.dillerCount < 17)
                {
                    diller.dillerCount += c.getCard(ref diller.dillerCard);
                    diller.CheckScoreWithTUZ();
                    Console.WriteLine("Diller got {0}, diller's count: {1}", diller.dillerCard, diller.dillerCount);
                }


                if ((player.playerCount > diller.dillerCount && player.playerCount <= 21) || (player.playerCount <= 21 && diller.dillerCount > 21))
                {
                    cash += rate * 1.5;
                    Console.WriteLine("You won {0}, your cash: {1}", rate * 1.5, cash);
                }
                else if (player.playerCount == diller.dillerCount)
                {
                    Console.WriteLine("Push, your cash: {0}", cash);
                }
                else
                {
                    cash -= rate;
                    Console.WriteLine("You lose {0}, your cash: {1}", rate, cash);
                }
                if (AskForExit()) break;
            }
        }
        
    }
}
