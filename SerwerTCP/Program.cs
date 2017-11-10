using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SerwerTCP
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
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
            while (true)
            {
                Socket cli = s.Accept();
                counter++;
                while (cli.Connected)
                {
                    try
                    {
                        Console.WriteLine("Połączenie z {0}", cli.RemoteEndPoint.ToString());
                        byte[] bufor = new byte[1024];
                        cli.Receive(bufor);
                        string temp = Encoding.UTF8.GetString(bufor).TrimEnd(new char[] { '\0' });
                        Console.WriteLine(temp.Trim());
                        foreach (char c in temp)
                        {
                            Console.WriteLine((int)c);
                        }
                        Console.WriteLine(temp.Length);

                        Byte[] sendBytes = Encoding.UTF8.GetBytes(temp);
                        cli.Send(sendBytes);
                    }
                    catch (SocketException)
                    {

                        cli.Close();
                        Console.WriteLine("Polaczenie niepodziewanie zamknięto.");
                    }
                }
                cli.Close();
                Console.WriteLine("Polaczenie zamknieto.");
            }

        }
    }
}
