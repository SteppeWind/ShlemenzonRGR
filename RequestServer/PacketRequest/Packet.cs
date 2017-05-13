using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestServer.PacketRequest
{
    public class Packet
    {
        public static int SizePacket => 10000;
        private static int bufferSizePacket = SizePacket * 8;
        public static int BufferSizePacket
        {
            get => bufferSizePacket;
            set
            {
                if (value > bufferSizePacket)
                {
                    bufferSizePacket = value;
                }
            }
        }

        public int NumberPacket { get; set; }

        public byte[] Bytes { get; set; }

        public string Id { get; set; }

        public static event Action<string> ExceptionRecieved;

        public int TotalCountPackets { get; set; }

        public Packet()
        {

        }

        public Packet(byte[] bytes, int number, string id, int maxPacketsCount)
        {
            Bytes = bytes;
            NumberPacket = number;
            Id = id;
            TotalCountPackets = maxPacketsCount;
        }

        public static Packet GetPacketFromJSON(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<Packet>(json);
            }
            catch (Exception ex)
            {
                ExceptionRecieved?.Invoke(ex.Message);
                return null;
            }
        }
        public static Packet GetPacketFromBytes(byte[] bytes) => GetPacketFromJSON(Encoding.UTF8.GetString(bytes, 0, bytes.Length));
        
    }
}