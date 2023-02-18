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
            outMessage = server.CreateMessage();
            new Mazes() { mazes = mazes }.PacketToNetOutGoingMessage(outMessage);
            server.SendMessage(outMessage, clientConnnection, NetDeliveryMethod.ReliableOrdered);
            server.FlushSendQueue();
            waiting = false;
            Console.WriteLine("connected");
        }
       
		
	}
}
