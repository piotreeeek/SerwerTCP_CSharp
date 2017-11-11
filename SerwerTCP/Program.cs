using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SerwerTCP
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ArrayList clientList = new ArrayList();
            bool flag = true;
            int port = 7;
            while (flag)
            {
                try
                {
                    Console.WriteLine("Propszę podaj numer portu.");
                    int intTemp = Convert.ToInt32(Console.ReadLine());
                    port = intTemp;
                    

                    s.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), port));
                    flag = false;
                    Console.WriteLine("Aktualny port: " + port + ".");
                }
                catch(SocketException)
                {
                    Console.WriteLine("Niestety port jest zajety. Spróbuj jeszcze raz.");                    
                }
                catch (FormatException)
                {
                    Console.WriteLine("Wpisane znaki nie są popranwym numerem. Spróbuj jeszcze raz.");
                }

            }

            s.Listen(5);

            int counter = 0;
            for(; ; )
            {
                Socket cli = s.Accept();
                clientList.Add(cli);
                ClientServerTheard clientServerTheard = new ClientServerTheard(cli);
                Thread thread = new Thread(clientServerTheard.Run);
                thread.Start();
            }
        }
    }
}
