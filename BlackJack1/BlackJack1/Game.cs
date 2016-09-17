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
            cards = new Cards();
        }

        

        bool AskForExit()
        {
            Console.Write("Stop game (Yes/No): ");
            string ans = Console.ReadLine();
            while (true)
            {
                if (ans.Equals("Yes"))
                {
                    return true;
                }
                else if (ans.Equals("No"))
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Incorrect answer, try again");
                }
            }
        }

        public void Play()
        {
            while(true)
            {
                Console.Write("Your cash: {0} Enter your rate: ", cash);
                rate = double.Parse(Console.ReadLine());

                Cards c = new Cards();
                Player player = new Player();
                Diller diller = new Diller();

                player.playerCount += c.getCard(ref player.myCard1);
                player.playerCount += c.getCard(ref player.myCard2);

                diller.dillerCount += c.getCard(ref diller.dillerCard);
                diller.dillerCount += c.getCard(ref diller.dillerCard2);

                diller.CheckScoreWithTUZ();
                player.CheckScoreWithTUZ();
                
                Console.WriteLine("Your cards is {0}, {1}; Diller's card is {2}", player.myCard1, player.myCard2, diller.dillerCard);

                string answer = "";

                if (player.playerCount == 21)
                {
                    Console.WriteLine("BlackJack!");
                    if (diller.dillerCard == Cards.Card.TUZ)
                    {
                        Console.WriteLine("1 to 1? (Yes/No): ");
                        answer = Console.ReadLine();
                        if (answer.Equals("Yes"))
                        {
                            cash += rate;
                            Console.WriteLine("You won {0}, your cash {1}", rate, cash);

                            if (AskForExit()) break;

                        }
                        else if (!answer.Equals("No"))
                        {
                            Console.WriteLine("Incorrect answer, try again");
                        }
                    }
                    else
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

                while (!answer.Equals("No"))
                {
                    Console.Write("Hit me (Yes/No): ");
                    answer = Console.ReadLine();
                    if (answer.Equals("Yes"))
                    {
                        player.playerCount += c.getCard(ref player.myCard1);
                        player.CheckScoreWithTUZ();
                        Console.WriteLine("Card is {0}, current score: {1}", player.myCard1, player.playerCount);
                    }
                    else if (!answer.Equals("No"))
                    {
                        Console.WriteLine("Incorrect answer, try again");
                    }
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
