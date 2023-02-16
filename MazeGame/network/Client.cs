using System;
using System.Collections.Generic;
using System.Text;
using Lidgren.Network;
using System.Threading;
//192.168.1.252
namespace MazeGame
{
    public class Client
    {
        string IP;
        public Client(string IPinput)
        {
            IP = IPinput;

            Thread thread = new Thread(Listen);
            thread.Start();

        }

        public void Listen()
        {
            var config = new NetPeerConfiguration("application name");
            var client = new NetClient(config);
            client.Start();
            client.Connect(host: IP, port: 3074);
            while (true)
            {
                var sendMessage = client.CreateMessage();
                string text = Console.ReadLine();
                sendMessage.Write(text);
                client.SendMessage(sendMessage, NetDeliveryMethod.ReliableOrdered);
                Console.Write(sendMessage.GetType());

                NetIncomingMessage message;
                while ((message = client.ReadMessage()) != null)
                {
                    Console.WriteLine(message.ReadString());
                }
            }

            


        }
    }
}
