using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack1
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game(1000);
            game.Play();
        }
    }
}
