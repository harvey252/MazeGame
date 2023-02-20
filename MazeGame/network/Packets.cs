using Lidgren.Network;

namespace Packets
{
    public enum PacketTypes
    {
        LocalPlayerPacket,
        PlayerDisconnectsPacket,
        PositionPacket,
        SpawnPacket,
        NewPlayer,
        WinTime,
        Rematch,
        Mazes,
        NextMaze
    }

    public interface IPacket
    {
        void PacketToNetOutGoingMessage(NetOutgoingMessage message);
        void NetIncomingMessageToPacket(NetIncomingMessage message);
    }

    public abstract class Packet : IPacket
    {
        public abstract void PacketToNetOutGoingMessage(NetOutgoingMessage message);
        public abstract void NetIncomingMessageToPacket(NetIncomingMessage message);
    }

    public class LocalPlayerPacket : Packet
    {
        public string ID { get; set; }

        public override void PacketToNetOutGoingMessage(NetOutgoingMessage message)
        {
            message.Write((byte)PacketTypes.LocalPlayerPacket);
            message.Write(ID);
        }

        public override void NetIncomingMessageToPacket(NetIncomingMessage message)
        {
            ID = message.ReadString();
        }
    }

    public class PlayerDisconnectsPacket : Packet
    {
        public string player { get; set; }

        public override void PacketToNetOutGoingMessage(NetOutgoingMessage message)
        {
            message.Write((byte)PacketTypes.PlayerDisconnectsPacket);
            message.Write(player);
        }

        public override void NetIncomingMessageToPacket(NetIncomingMessage message)
        {
            player = message.ReadString();
        }
    }

    //contains the postion of another player
    public class PositionPacket : Packet
    {
        public float X { get; set; }
        public float Y { get; set; }


        public override void PacketToNetOutGoingMessage(NetOutgoingMessage message)
        {
            message.Write((byte)PacketTypes.PositionPacket);
            message.Write(X);
            message.Write(Y);

        }

        public override void NetIncomingMessageToPacket(NetIncomingMessage message)
        {
            X = message.ReadFloat();
            Y = message.ReadFloat();

        }
    }

    //contains the colour and name of a new player
    public class NewPlayer : Packet
    {
        public string colour { get; set; }
        public string name { get; set; }
        public override void PacketToNetOutGoingMessage(NetOutgoingMessage message)
        {
            message.Write((byte)PacketTypes.NewPlayer);
            message.Write(colour);
            message.Write(name);
        }

        public override void NetIncomingMessageToPacket(NetIncomingMessage message)
        {
            colour = message.ReadString();
            name = message.ReadString();
        }

    }
    //contains the time in seconds and min a win is made
    public class WinTime : Packet
    {
        public int seconds { get; set; }
        public int min { get; set; }
        public override void PacketToNetOutGoingMessage(NetOutgoingMessage message)
        {
            message.Write((byte)PacketTypes.WinTime);
            message.Write(seconds);
            message.Write(min);
        }

        public override void NetIncomingMessageToPacket(NetIncomingMessage message)
        {
            seconds = message.ReadInt32();
            min = message.ReadInt32();
        }

    }

    //contains weather the user would like a rematch
    public class Rematch : Packet
    {
        public bool answer { get; set; }

        public override void PacketToNetOutGoingMessage(NetOutgoingMessage message)
        {
            message.Write((byte)PacketTypes.NewPlayer);
            message.Write(answer);
        }

        public override void NetIncomingMessageToPacket(NetIncomingMessage message)
        {
            answer = message.ReadBoolean();

        }

    }

    //contains the mazes the player will use
    public class Mazes : Packet
    {
        public string mazeType;
        public int[][,] mazes { get; set; }
       

        //needed to know how meny times to read from packet
        private int numberOfMazes;
  

        public override void PacketToNetOutGoingMessage(NetOutgoingMessage message)
        {
            
            //mazes
            message.Write((byte)PacketTypes.Mazes);
            message.Write(mazeType);
            numberOfMazes = mazes.Length;
            message.Write(numberOfMazes);
            //converts mazes to strings
            foreach(int[,] maze in mazes)
            {
                message.Write(MazeGame.MazeGenerator.toString(maze));
            }


       
        }

        public override void NetIncomingMessageToPacket(NetIncomingMessage message)
        {
            mazeType = message.ReadString();
            //mazes
            numberOfMazes = message.ReadInt32();
            mazes = new int[numberOfMazes][,];
            //converts mazes back to int[,]
            for(int n = 0; n < numberOfMazes; n += 1)
            {
                mazes[n] = MazeGame.MazeGenerator.fromString(message.ReadString());
            }

        }


        

        }
    public class NextMaze : Packet
    {
        public int index { get; set; }

        public override void PacketToNetOutGoingMessage(NetOutgoingMessage message)
        {
            message.Write((byte)PacketTypes.NewPlayer);
            message.Write(index);
        }

        public override void NetIncomingMessageToPacket(NetIncomingMessage message)
        {
            index = message.ReadInt32();

        }
    }

}