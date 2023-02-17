using System;
using System.Collections.Generic;
using System.Text;
using Lidgren.Network;
using System.Threading;
using Packets;
//192.168.1.252
//192.168.1.252
namespace MazeGame
{
    public class Client
    {
        string IP;
        string name;
        string color;
        public Client(string IPinput,string inputName,string inputColor)
        {
            name = inputName;
            color = inputColor;
            IP = IPinput;

            Thread thread = new Thread(Listen);
            thread.Start();

        }

        public void Listen()
        {
            var config = new NetPeerConfiguration("application name");
            var client = new NetClient(config);
            client.Start();
            client.Connect(host: "192.168.1.252", port: 433);
            client.FlushSendQueue();

            //sending start packet



            NetOutgoingMessage startmessage = client.CreateMessage();
            new NewPlayer() { name = name, colour = color }.PacketToNetOutGoingMessage(startmessage);
            
            client.SendMessage(startmessage, NetDeliveryMethod.ReliableOrdered);

            client.FlushSendQueue();


            //phase one waiting for connection
            NetIncomingMessage message;
            bool waiting = true;
            while (waiting)
            {
                if ((message = client.ReadMessage()) != null)
                {


                    if ((int)message.ReadByte() == (int)PacketTypes.NewPlayer)
                    {
                        NewPlayer packet = new NewPlayer();
                        packet.NetIncomingMessageToPacket(message);
                        Console.WriteLine(packet.name + " " + packet.colour);
                        waiting = false;
                        Console.WriteLine("connected");
                    }
                }
                else
                {
                    startmessage = client.CreateMessage();
                    new NewPlayer() { name = name, colour = color }.PacketToNetOutGoingMessage(startmessage);
                    client.SendMessage(startmessage, NetDeliveryMethod.ReliableOrdered);
                    client.FlushSendQueue();
                }
            }
        }
    }
}
