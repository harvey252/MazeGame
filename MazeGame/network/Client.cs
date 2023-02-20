using System;
using System.Collections.Generic;
using System.Text;
using Lidgren.Network;
using System.Threading;
using Packets;
//192.168.1.252
//192.168.1.252
//10.210.198.91
namespace MazeGame
{
    public class Client
    {
        private string IP;
        private string clientName;
        private string clientColor;
        private NetClient client;

        //public varables
        public bool waiting = true;
        public int[][,] hostMazes;
        public int[][,] clientMazes;

        public string hostName;
        public string hostColor;


        public Client(string IPinput,string inputName,string inputColor)
        {
            clientName = inputName;
            clientColor = inputColor;
            IP = IPinput;

            Thread thread = new Thread(Listen);
            thread.Start();

        }

        public void Listen()
        {
            var config = new NetPeerConfiguration("application name");
            client = new NetClient(config);
            client.Start();
            client.Connect(host: "10.210.198.91", port: 433);
            client.FlushSendQueue();

            //sending start packet



            NetOutgoingMessage startmessage = client.CreateMessage();
            new NewPlayer() { name = clientName, colour = clientColor }.PacketToNetOutGoingMessage(startmessage);
            
            client.SendMessage(startmessage, NetDeliveryMethod.ReliableOrdered);

            client.FlushSendQueue();


            //phase one waiting for connection
            NetIncomingMessage message;
            waiting = true;
            while (waiting)
            {
                if ((message = client.ReadMessage()) != null)
                {
                    var type = (int)message.ReadByte();

                    if (type == (int)PacketTypes.NewPlayer)
                    {
                        NewPlayer packet = new NewPlayer();
                        packet.NetIncomingMessageToPacket(message);
                        Console.WriteLine(packet.name + " " + packet.colour);
                        hostName = packet.name;
                        hostColor = packet.colour;

                        hostName = packet.name;
                        hostColor = packet.colour;
                        if (clientMazes != null&&hostMazes!=null)
                        {
                            waiting = false;
                        }
                        Console.WriteLine("connected");
                    }
                    else if (type == (int)PacketTypes.Mazes)
                    {
                        Mazes packet = new Mazes();
                        packet.NetIncomingMessageToPacket(message);
                        if(packet.mazeType=="both")
                        {
                            Console.WriteLine("playing with same mazes");
                            clientMazes = packet.mazes;
                            hostMazes = packet.mazes;
                        }
                        else if(packet.mazeType =="host")
                        {
                            Console.WriteLine("resived host mazes");
                            hostMazes = packet.mazes;
                        }
                        else
                        {
                            Console.WriteLine("resived client mazes");
                            clientMazes = packet.mazes;
                        }


                        if (hostName != null&& clientMazes != null && hostMazes != null)
                        {
                            waiting = false;
                            Console.WriteLine("resived mazes");
                        }
                        
                        
                    }
                }
                else
                {
                    startmessage = client.CreateMessage();
                    new NewPlayer() { name = clientName, colour = clientColor }.PacketToNetOutGoingMessage(startmessage);
                    client.SendMessage(startmessage, NetDeliveryMethod.ReliableOrdered);
                    client.FlushSendQueue();
                }
            }



        }
        public void sendpostion(float x, float y)
        {
            NetOutgoingMessage outMessage = client.CreateMessage();
            new PositionPacket() { X = x, Y = y }.PacketToNetOutGoingMessage(outMessage);
            client.SendMessage(outMessage, NetDeliveryMethod.ReliableOrdered);
            client.FlushSendQueue();
        }

        public void sendWin()
        {
            NetOutgoingMessage outMessage = client.CreateMessage();
            new WinTime() { seconds = DateTime.Now.Second, min = DateTime.Now.Minute }.PacketToNetOutGoingMessage(outMessage);
            client.SendMessage(outMessage, NetDeliveryMethod.ReliableOrdered);
            client.FlushSendQueue();
        }
    }
}
