using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace P2PChat
{
    class Member
    {
        List<Socket> clients;
        Socket listener;
        String name;
        int messages;
        String myaddr;
        List<string> ids;

        public Member(string nick, string addr, int port, string myaddr, int myport)
        {
            clients = new List<Socket>();
            if (!addr.Equals(""))
            {
                Connect(addr, port);
            }

            messages = 0;
            ids = new List<string>();
            
            listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(new IPEndPoint(IPAddress.Parse(myaddr), myport));
            listener.Listen(1000);
            name = nick;
            this.myaddr = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString() + myport.ToString();
        }

        void Connect(string addr, int port)
        {
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sock.Connect(addr, port);
            clients.Add(sock);
        }

        public void Start()
        {
            while (true)
            {
                try
                {
                    if (listener.Poll(5000, SelectMode.SelectRead))
                    {
                        Socket sock = listener.Accept();
                        clients.Add(sock);
                    }
                    List<Socket> list = new List<Socket>(clients), list2;
                    if (list.Count > 0) Socket.Select(list, null, null, 5000);
                    foreach (Socket sock in list)
                    {
                        try
                        {
                            string inf = new StreamReader(new NetworkStream(sock)).ReadLine();

                            string id = inf.Substring(0, inf.IndexOf(' '));

                            if (ids.Exists(x => x == id)) continue;
                            ids.Add(id);

                            list2 = new List<Socket>(clients);
                            if (list2.Count > 0) Socket.Select(null, list2, null, 5000);
                            foreach (Socket sock2 in list2)
                            {
                                if (sock2 == sock) continue;
                                StreamWriter sr = new StreamWriter(new NetworkStream(sock2));
                                sr.WriteLine(inf);
                                sr.Flush();
                            }
                            Console.WriteLine(inf.Substring(inf.IndexOf(' ') + 1)); ;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            clients.Remove(sock);
                        }
                    }

                    ConsoleKey key = ConsoleKey.NoName;
                    if(Console.KeyAvailable) key = Console.ReadKey().Key;

                    if (key == ConsoleKey.Enter)
                    {
                        Console.Write("{0}: ", name);

                        ids.Add(myaddr + messages.ToString());

                        string msg = Console.ReadLine();
                        list = new List<Socket>(clients);
                        if (list.Count > 0) Socket.Select(null, list, null, 5000);
                        foreach (Socket sock in list)
                        {
                            try
                            {
                                StreamWriter sr = new StreamWriter(new NetworkStream(sock));
                                sr.WriteLine("{0} {1}: {2}", myaddr+messages.ToString(), name, msg);
                                sr.Flush();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                clients.Remove(sock);
                            }
                        }
                        messages++;
                    }
                    else if (key == ConsoleKey.F3)
                    {
                        Console.WriteLine("Connect to remote host, port: ");
                        string host = Console.ReadLine();
                        int port = int.Parse(Console.ReadLine());
                        Connect(host, port);
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }
    }
}
