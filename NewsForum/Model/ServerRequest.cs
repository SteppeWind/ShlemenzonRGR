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
using System.Threading;
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

        private static event Action IsEnded;

        public static int Delay { get; private set; } = 5;
        

        private static List<Packet> ListPackets = new List<Packet>();

        public async static Task ConnectToServer()
        {
            try
            {
                Socket = new StreamSocket();
                HostName host = new HostName(ServerSettings.Host);
                await Socket.ConnectAsync(host, ServerSettings.Port.ToString());
                IsEnded += ServerRequest_IsEnded;
                IsConnect = true;
            }
            catch (Exception)
            {
                IsConnect = false;
            }
        }

        public async static Task<Answer> SendRequest(MainRequest mainRequest)
        {
            Answer answer = null;
            if (mainRequest.RecievedRequest is VMMusicPublication)
            {
                Delay = 50;
            }
            else
            {
                Delay = 5;
            }

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
                    await Task.Delay(Delay);
                    await OutputStream.WriteAsync(item, 0, item.Length);
                    await OutputStream.FlushAsync();
                }
                CancellationTokenSource source = new CancellationTokenSource();
                var token = source.Token;
                try
                {
                    while (true)
                    {
                        byte[] pieceAnswerBytes = new byte[Packet.BufferSizePacket];
                        int pieceAnswerSize = await InputStream.ReadAsync(pieceAnswerBytes, 0, pieceAnswerBytes.Length, token);
                        if (token.IsCancellationRequested)
                            break;
                        DataProcessing((pieceAnswerBytes, pieceAnswerSize), source);
                    }
                }
                catch (Exception ex)
                {
                    IsConnect = false;
                }
                
                answer = Answer.GetTObjFromPackets(ListPackets.OrderBy(p => p.NumberPacket).ToList());
                ListPackets.Clear();
            }

            return answer;
        }

        public static async void DataProcessing((byte[] bytes, int size) item, CancellationTokenSource sourceToken)
        {
            var packet = await Packet.GetPacketFromFixBytesAsync(item.bytes, item.size);
            lock (ListPackets)
            {
                if (packet != null)
                {
                    ListPackets.Add(packet);
                    if (packet.TotalCountPackets == ListPackets.Count)
                    {
                        sourceToken.Cancel();
                    }
                }
            }
        }


        private static void ServerRequest_IsEnded()
        {
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