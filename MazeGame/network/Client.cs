using System;
using System.Collections.Generic;
using System.Text;
using Lidgren.Network;
using System.Threading;

namespace MazeGame
{
    public class Client
    {
     
        public Client()
        {
            

            Thread thread = new Thread(Listen);
            thread.Start();

        }

        public void Listen()
        {
            var config = new NetPeerConfiguration("application name");
            var client = new NetClient(config);
            client.Start();
            client.Connect(host: "192.168.1.252", port: 3074);
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
