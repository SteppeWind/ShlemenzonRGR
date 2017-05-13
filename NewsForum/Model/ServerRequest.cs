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

        public async static Task ConnectToServer()
        {
            try
            {
                Socket = new StreamSocket();
                HostName host = new HostName(ServerSettings.Host);
                await Socket.ConnectAsync(host, ServerSettings.Port.ToString());
                IsConnect = true;
            }
            catch (Exception)
            {
                IsConnect = false;
            }
        }

        public async static Task<Answer> SendRequest(MainRequest mainRequest)
        {
            if (!IsConnect)
            {
                await ConnectToServer();
            }
            if (IsConnect)
            {
                var collection = MainRequest.GetTObjectPacketsBytes(mainRequest);
                foreach (var item in collection)
                {
                    await OutputStream.WriteAsync(item, 0, item.Length);
                    await OutputStream.FlushAsync();
                }

                return await ReadAnswer();
            }
            return null;
        }
        
        private async static Task<Answer> ReadAnswer()
        {
            List<Packet> listPackets = new List<Packet>();
            while (true)
            {
                byte[] pieceAnswerBytes = new byte[Packet.BufferSizePacket];
                int pieceAnswerSize = await InputStream.ReadAsync(pieceAnswerBytes, 0, pieceAnswerBytes.Length);
                Array.Resize(ref pieceAnswerBytes, pieceAnswerSize);
                Packet packet = Packet.GetPacketFromBytes(pieceAnswerBytes);
                listPackets.Add(packet);
                if (packet.NumberPacket == packet.TotalCountPackets)
                {
                    break;
                }
            }

            return Answer.GetTObjFromPackets(listPackets);
        }
    }
}