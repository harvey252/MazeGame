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
        private int[][,] mazes;
		public Host(string inputName, string inputColor,int[][,] inpMazes)
        {
            name = inputName;
            color = inputColor;
            mazes = inpMazes;
            thread = new Thread(Listen);
			thread.Start();
               
        }

		public void Listen()
        { 
            var config = new NetPeerConfiguration("application name")
            { Port = 433 };
            var server = new NetServer(config);
            
            server.Start();
            bool waiting = true;
            NetIncomingMessage message;


            while (waiting)
            {
                if((message = server.ReadMessage()) != null)
                { 
                    

                    if ((int)message.ReadByte() == (int)PacketTypes.NewPlayer)
                    {

                        clientConnnection = message.SenderConnection;
                        NewPlayer packet=new NewPlayer(); 
                        //i think error is here
                        packet.NetIncomingMessageToPacket(message);
                        Console.WriteLine(packet.name+" "+packet.colour);

                        //reponds with same packet
                        NetOutgoingMessage outMessage = server.CreateMessage();
                        new NewPlayer() { name = name, colour = color }.PacketToNetOutGoingMessage(outMessage);
                        server.SendMessage(outMessage,clientConnnection, NetDeliveryMethod.ReliableOrdered);
                        server.FlushSendQueue();
                        //reponds with mazes
                        //outMessage = server.CreateMessage();
                        //new Mazes() { mazes = mazes }.PacketToNetOutGoingMessage(outMessage);
                        //server.SendMessage(outMessage, server.Connections[0], NetDeliveryMethod.ReliableOrdered);
                        //server.FlushSendQueue();

                        
                    }
                    else
                    {
                        Console.WriteLine(message.ReadString());
                    }
                }
            }

    
        }
		
	}
}
