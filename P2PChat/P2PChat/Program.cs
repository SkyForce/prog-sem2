using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace P2PChat
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter your nickname and your port: ");
            string nick = Console.ReadLine(), host="";
            int myport = int.Parse(Console.ReadLine()), rport = 0;

            while (true)
            {
                Console.Write("Are you first user? (y/n): ");
                char c = (char) Console.Read();
                Console.ReadLine();
                if (c == 'n')
                {
                    Console.WriteLine("Enter remote host, port: ");
                    host = Console.ReadLine();
                    string n = Console.ReadLine();
                    rport = int.Parse(n);
                }
                Member m;
                try
                {
                    m = new Member(nick, host, rport, "127.0.0.1", myport);
                    m.Start();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }
    }
}
