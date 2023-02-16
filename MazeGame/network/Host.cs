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


			public Host()
			{
			
			thread = new Thread(Listen);
				thread.Start();
               
            }

			public void Listen()
			{
                var config = new NetPeerConfiguration("application name")
                { Port = 3074 };
                var server = new NetServer(config);

                server.Start();
                NetIncomingMessage message;
                while (true)
                {
                    while ((message = server.ReadMessage()) != null)
                    {


                        Console.WriteLine(NetUtility.ToHexString(message.SenderConnection.RemoteUniqueIdentifier));
                        Console.WriteLine(message.ReadString());

                        var newmessage = server.CreateMessage();
                        newmessage.Write(Console.ReadLine());
                        server.SendMessage(newmessage, message.SenderConnection, NetDeliveryMethod.ReliableOrdered);

                    }
              
                
                }

            }


            
		
	}
}
