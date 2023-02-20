using System;
using System.Collections.Generic;
using System.Threading;
using Lidgren.Network;
using Packets;

namespace MazeGame
{
	public class Host
	{


	    private Thread thread;
        private NetConnection clientConnnection;

        private string name;
        private string color;
        private int[][,] hostMazes;
        private int[][,] clientMazes;
        private NetServer server;

        // public varables
        public bool waiting=true;
        public string clientName;
        public string clientColor;
        
		public Host(string inputName, string inputColor,int[][,] inpHostMazes,int[][,]inpClientMazes)
        {
            name = inputName;
            color = inputColor;
            hostMazes = inpHostMazes;
            clientMazes = inpClientMazes;
            thread = new Thread(Listen);
			thread.Start();
               
        }

		public void Listen()
        {
            Console.WriteLine("wating to connect");
            var config = new NetPeerConfiguration("application name")
            { Port = 433 };
            server = new NetServer(config);
            
            server.Start();
            waiting = true;
            NetIncomingMessage message;
            NetOutgoingMessage outMessage;

            while (waiting)
            {
                if((message = server.ReadMessage()) != null)
                { 
                    
                    //cannot tell between packet types?
                    if (message.MessageType == NetIncomingMessageType.Data&& message.ReadByte()==(byte)PacketTypes.NewPlayer)
                    {
                        
                        clientConnnection = message.SenderConnection;
                        NewPlayer packet=new NewPlayer(); 
                        //i think error is here
                        packet.NetIncomingMessageToPacket(message);
                        Console.WriteLine(packet.name+" "+packet.colour);
                        clientName = packet.name;
                        clientColor = packet.colour;

                        //reponds with same packet
                        outMessage = server.CreateMessage();
                        new NewPlayer() { name = name, colour = color }.PacketToNetOutGoingMessage(outMessage);
                        server.SendMessage(outMessage,clientConnnection, NetDeliveryMethod.ReliableOrdered);
                        server.FlushSendQueue();

                        waiting = false;
                        
                    }
                    else
                    {
                       // Console.WriteLine(message.ReadString());
                    }
                }
            }

            //reponds with mazes
            
            outMessage = server.CreateMessage();

            if (clientMazes == hostMazes)
            {
                new Mazes() { mazes = clientMazes, mazeType = "both" }.PacketToNetOutGoingMessage(outMessage);
                server.SendMessage(outMessage, clientConnnection, NetDeliveryMethod.ReliableOrdered);
                server.FlushSendQueue();
            }
            else
            {
                new Mazes() { mazes = clientMazes, mazeType = "client" }.PacketToNetOutGoingMessage(outMessage);
                server.SendMessage(outMessage, clientConnnection, NetDeliveryMethod.ReliableOrdered);
                server.FlushSendQueue();
                outMessage = server.CreateMessage();
                new Mazes() { mazes = clientMazes, mazeType = "host" }.PacketToNetOutGoingMessage(outMessage);
                server.SendMessage(outMessage, clientConnnection, NetDeliveryMethod.ReliableOrdered);
                server.FlushSendQueue();
            }
            waiting = false;
            Console.WriteLine("connected");
        }

        public void sendpostion(float x, float y)
        {
            NetOutgoingMessage outMessage = server.CreateMessage();
            new PositionPacket() {X=x,Y=y}.PacketToNetOutGoingMessage(outMessage);
            server.SendMessage(outMessage, clientConnnection, NetDeliveryMethod.ReliableOrdered);
            server.FlushSendQueue();
        }

        public void sendWin()
        {
            NetOutgoingMessage outMessage = server.CreateMessage();
            new WinTime() { seconds = DateTime.Now.Second,min = DateTime.Now.Minute }.PacketToNetOutGoingMessage(outMessage);
            server.SendMessage(outMessage, clientConnnection, NetDeliveryMethod.ReliableOrdered);
            server.FlushSendQueue();
        }
    }
}
