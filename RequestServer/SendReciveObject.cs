using Newtonsoft.Json;
using RequestServer.PacketRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestServer
{
    public class SendReciveObject<T> where T : class
    {
        public static List<byte[]> GetTObjectPacketsBytes(T tObj)
        {
            byte[] bytesRequest = SerializeObject(tObj, true);
            int bytesRequestLength = bytesRequest.Length;
            int packetSize = Packet.SizePacket;
            int maxCountPackets = bytesRequestLength / packetSize + (bytesRequestLength % packetSize != 0 ? 1 : 0);

            string guid = Guid.NewGuid().ToString();
            List<byte[]> resultList = new List<byte[]>();
            for (int i = 0, j = 0; bytesRequestLength - i > 0; i += packetSize, j++)
            {
                Packet packet = new Packet(bytesRequest.Skip(j * packetSize).Take(packetSize).ToArray(), j + 1, guid, maxCountPackets);
                byte[] packetBytes = SerializeObject(packet);
                resultList.Add(packetBytes);
            }
            if (resultList.Count > 0)
                Packet.BufferSizePacket = resultList[0].Length;

            return resultList;
        }

        public static T GetTObjFromPackets(List<Packet> listPackets)
        {
            var tObj = listPackets.Select(p => p.Bytes).Aggregate((x, y) => x.Concat(y).ToArray());
            return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(tObj, 0, tObj.Length));
        }

        public static byte[] SerializeObject(object tObj, bool IsInhertance = false)
        {
            if (IsInhertance)
                return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(tObj, Formatting.Indented, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All }));
            else
                return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(tObj));
        }
    }
}
