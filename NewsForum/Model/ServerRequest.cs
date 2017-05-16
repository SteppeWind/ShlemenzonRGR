using Newtonsoft.Json;
using RequestServer.AnswerForRequest;
using RequestServer.PacketRequest;
using RequestServer.PublicationsRequest;
using RequestServer.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelDataBase.VMPublicationTypes;
using Windows.Networking;
using Windows.Networking.Sockets;

namespace NewsForum.Model
{
    static class ServerRequest
    {
        private static StreamSocket Socket;
        private static Stream OutputStream => Socket?.OutputStream.AsStreamForWrite();
        private static Stream InputStream => Socket?.InputStream.AsStreamForRead();
        public static bool IsConnect { get; private set; } = false;
        private static event Action<(byte[] bytes, int size)> DataRecieved;
        public static event Action<Answer> GetAnswerForLastRequest;

        public async static Task ConnectToServer()
        {
            try
            {
                Socket = new StreamSocket();
                HostName host = new HostName(ServerSettings.Host);
                await Socket.ConnectAsync(host, ServerSettings.Port.ToString());
                IsConnect = true;
                DataRecieved += ServerRequest_DataRecieved;
                ReadAnswer();
            }
            catch (Exception)
            {
                IsConnect = false;
            }
        }

        private static List<Packet> listPackets = new List<Packet>();

        private async static void ServerRequest_DataRecieved((byte[] bytes, int size) item)
        {
            var packet = await Packet.GetPacketFromFixBytes(item.bytes, item.size);
            lock (listPackets)
            {
                if (packet != null)
                {
                    listPackets.Add(packet);
                    if (packet.TotalCountPackets == listPackets.Count)
                    {
                        GetAnswerForLastRequest?.Invoke(Answer.GetTObjFromPackets(listPackets.OrderBy(p => p.NumberPacket).ToList()));
                        listPackets.Clear();
                    }
                }
            }
        }

        
        public async static Task SendRequest(MainRequest mainRequest)
        {
            List<Packet> tempListPackets = new List<Packet>();
            if (!IsConnect)
            {
                await ConnectToServer();
            }
            if (IsConnect)
            {
                var collection = MainRequest.GetTObjectPacketsBytes(mainRequest);
                foreach (var item in collection)
                {
                    await Task.Delay(1);
                    await OutputStream.WriteAsync(item, 0, item.Length);
                    await OutputStream.FlushAsync();
                }
            }
        }

        private async static void ReadAnswer()
        {
            while (true)
            {
                byte[] pieceAnswerBytes = new byte[Packet.BufferSizePacket];
                int pieceAnswerSize = await InputStream.ReadAsync(pieceAnswerBytes, 0, pieceAnswerBytes.Length);
                DataRecieved((pieceAnswerBytes, pieceAnswerSize));
            }
         }
    }
}