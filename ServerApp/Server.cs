using Newtonsoft.Json;
using RequestServer.AnswerForRequest;
using RequestServer.PacketRequest;
using RequestServer.PublicationsRequest;
using RequestServer.Request;
using ServerApp.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ViewModelDataBase.VMPublicationTypes;

namespace ServerApp
{
    class Server
    {
        public static string PathServerDirecotory = @"F:\учеба\3 курс\2ой семестр\Шлемензон_всё\ргр\ServerFiles";
        public string Host { get; private set; } = ServerSettings.Host;
        public int Port { get; private set; } = ServerSettings.Port;
        public event Action<string> ExceptionRecived;
        private int SizeBuffer = Packet.SizePacket * 2;

        private TcpListener ListenerConnections;

        public Server()
        {
            start();
        }

        public Server(string host, int port)
        {
            Host = host;
            Port = port;
            
        }

        void start()
        {
            ListenerConnections = new TcpListener(IPAddress.Any, Port);
            ListenerConnections.Start();
            StartListening();
        }

        public async void StartListening()
        {
            while (true)
            {
                var tcpClient = await ListenerConnections.AcceptTcpClientAsync();
                Client client = new Client(tcpClient);
                client.RecievedRequest += Client_RecievedRequest;
                client.ExceptionRecieved += Client_ExceptionRecieved;
                client.NumPacketRecieved += Client_NumPacketRecieved;
            }
            //while (true)
            //{
            //    Socket handler = SListener.Accept();
            //    byte[] bytes = new byte[SizeBuffer];
            //    int bytesRecive = handler.Receive(bytes);

            //}
        }

        private void Client_NumPacketRecieved(int obj, int max)
        {
            Console.WriteLine($"Номер пакета - {obj} из {max}");
        }

        private void Client_ExceptionRecieved(string obj)
        {
            ExceptionRecived?.Invoke(obj);
        }

        private void Client_RecievedRequest(Client client, MainRequest Request)
        {
            Answer answer;
            bool result = false;
            switch (Request.TypeRequest)
            {
                case TypeRequest.Update:
                    break;
                case TypeRequest.Delete:
                    break;
                case TypeRequest.Create:
                    var request = JsonConvert.DeserializeObject<CreatePublicationRequest>(Request.RecievedRequest.ToString(), new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
                    result = PublicationCRUD.CreatePublication(request.NewPublication);
                    answer = new Answer()
                    {
                        TypeAnswer = TypeAnswer.Bool,
                        SelfAnswer = result,
                    };

                    break;
                case TypeRequest.Read:
                    answer = new Answer()
                    {
                        TypeAnswer = TypeAnswer.Bool,
                        DataType = RequestServer.DataType.Publication,
                        SelfAnswer = result,
                    };
                    client.SendAnswer(answer);
                    break;
                default:
                    break;
            }


        }
    }

    class Client
    {
        private TcpClient TcpClient;
        private NetworkStream NetWorkStream;
        private int SizeBuffer = Packet.BufferSizePacket;
        public delegate void RecievedRequestEventHandler(Client client, MainRequest Request);
        public event RecievedRequestEventHandler RecievedRequest;
        public event Action<string> ExceptionRecieved;
        public event Action<int, int> NumPacketRecieved;

        public Client(TcpClient tcp)
        {
            TcpClient = tcp;
            NetWorkStream = tcp.GetStream();
            ReadStreamDataAsync();
        }

        public async void ReadStreamDataAsync()
        {
            List<Packet> listPackets = new List<Packet>();
            Queue<Tup>
            while (true)
            {
                byte[] bytes = new byte[SizeBuffer];
                if (NetWorkStream.CanRead)
                {
                    int sizePiecePacket = await NetWorkStream.ReadAsync(bytes, 0, bytes.Length);
                    var packet = await GetPacket(bytes, sizePiecePacket);
                    if (packet != null)
                    {
                        listPackets.Add(packet);
                        if (packet.NumberPacket == packet.TotalCountPackets)
                        {
                            RecievedRequest(this, MainRequest.GetTObjFromPackets(listPackets));
                            listPackets.Clear();
                        }
                    }
                }
            }
        }

        public Task<Packet> GetPacket(byte[] bytes, int sizePiecePacket)
        {
            try
            {
                return Task.Run(() =>
                {
                    Array.Resize(ref bytes, sizePiecePacket);
                    string json = Encoding.UTF8.GetString(bytes);
                    Packet packet = Packet.GetPacketFromBytes(bytes);
                    Packet.ExceptionRecieved += Packet_ExceptionRecieved;
                    if (packet != null)
                        NumPacketRecieved(packet.NumberPacket, packet.TotalCountPackets);
                    return packet;
                });
            }
            catch (Exception ex)
            {
                ExceptionRecieved(ex.Message);
            }
            return null;
        }

        private void Packet_ExceptionRecieved(string obj)
        {
            ExceptionRecieved(obj);
        }

        public async void SendAnswer(Answer answer)
        {
            var collection = Answer.GetTObjectPacketsBytes(answer);
            foreach (var item in collection)
            {
                await NetWorkStream.WriteAsync(item, 0, item.Length);
            }
        }
    }
}