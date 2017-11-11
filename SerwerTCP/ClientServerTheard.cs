using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;

namespace SerwerTCP
{
    class ClientServerTheard 
    {
        Socket clientSocket;
        bool running = true;
        byte[] messageByte = new byte[1024];

        public ClientServerTheard(Socket s)
        {
            clientSocket = s;
        }

        public void Run()
        {
            Socket s = this.clientSocket;
            Console.WriteLine("Nowe połączenie z " + s.RemoteEndPoint.ToString());
            try
            {
                while (this.running)
                {
                    int length= s.Receive(messageByte);
                    Console.WriteLine(length);
                    byte[] sendByte = new List<byte>(messageByte).GetRange(0, length-1).ToArray();
                    Console.WriteLine(sendByte.Length);
                    string message = Encoding.UTF8.GetString(sendByte);
                    if (message.ToLower().Equals("quit"))
                    {
                        this.running = false;
                        Console.WriteLine("Zatrzymano wątek kienta dla klienta " + s.RemoteEndPoint.ToString());
                    }
                    else
                    {
                        s.Send(sendByte);
                        Console.WriteLine("Wiadomośc od klienta " + s.RemoteEndPoint.ToString() + " : " + message);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Klient " + this.clientSocket.RemoteEndPoint.ToString() + " rozłaczony.");
            }
            try
            {
                this.clientSocket.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }



    }
}
