using Newtonsoft.Json;
using RequestServer.AnswerForRequest;
using RequestServer.PacketRequest;
using RequestServer.PublicationsRequest;
using RequestServer.Request;
using ServerApp.CRUD;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ViewModelDataBase.VMPublicationTypes;
using System.Collections.Concurrent;
using ViewModelDataBase.VMPublicationTypes.VMNewsTypes;

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
            switch (Request.DataType)
            {
                case RequestServer.DataType.Publication:
                    PublicationRequests(Request);
                    break;
                case RequestServer.DataType.User:
                    UserRequests(Request);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Обработка запроса, поступившая от админа
        /// </summary>
        /// <param name="mainRequest">Запрос с данными</param>
        private void UserRequests(MainRequest mainRequest)
        {
            switch (mainRequest.TypeRequest)
            {
                case TypeRequest.Update:
                    break;
                case TypeRequest.Delete:
                    break;
                case TypeRequest.Create:
                    break;
                case TypeRequest.Read:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Обработка создания, удаления, обновления, чтения публикаций
        /// </summary>
        /// <param name="mainRequest"></param>
        private void PublicationRequests(MainRequest mainRequest)
        {
            object result = null;
            Answer answer = new Answer() { SelfAnswer = result };
            string json = mainRequest.RecievedRequest.ToString();
            var @public = JsonConvert.DeserializeObject<VMPublication>(json);
            switch (mainRequest.TypeRequest)
            {
                case TypeRequest.Update:
                    break;
                case TypeRequest.Delete:
                    break;
                case TypeRequest.Create:
                    VMPublication castPublic = null;
                    switch (@public.TypePublication)
                    {
                        case Model.PublicationTypes.PublicationType.Game:
                            castPublic= JsonConvert.DeserializeObject<VMGamePublication>(json);
                            break;
                        case Model.PublicationTypes.PublicationType.Film:
                            castPublic = JsonConvert.DeserializeObject<VMFilmPublication>(json);
                            break;
                        case Model.PublicationTypes.PublicationType.Music:
                            castPublic = JsonConvert.DeserializeObject<VMMusicPublication>(json);
                            break;
                        case Model.PublicationTypes.PublicationType.News:
                            castPublic = JsonConvert.DeserializeObject<VMNewsPublication>(json);
                            break;
                        default:
                            break;
                    }
                    result = PublicationCRUD.CreatePublication(castPublic);
                    break;
                case TypeRequest.Read:
                    answer.TypeAnswer = TypeAnswer.Publications;
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
        //public event Action<string> ExceptionRecieved;
        //public event Action<int, int> NumPacketRecieved;
        private event Action<(byte[] bytes, int size)> DataRecieved;

        public Client(TcpClient tcp)
        {
            TcpClient = tcp;
            NetWorkStream = tcp.GetStream();
            ReadStreamDataAsync();
            DataRecieved += Client_DataRecieved;
        }

        List<Packet> listPackets = new List<Packet>();
        private async void Client_DataRecieved((byte[] bytes, int size) item)
        {
            try
            {
                var packet = await Packet.GetPacketFromFixBytes(item.bytes, item.size);
                lock (listPackets)
                {
                    if (packet != null)
                    {
                        listPackets.Add(packet);
                        if (listPackets.Count == packet.TotalCountPackets)
                        {
                            var res = MainRequest.GetTObjFromPackets(listPackets.OrderBy(el => el.NumberPacket).ToList());
                            RecievedRequest(this, res);
                            listPackets.Clear();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }                          
        }
       
        public async void ReadStreamDataAsync()
        {
            try
            {
                while (true)
                {
                    byte[] bytes = new byte[SizeBuffer];
                    int sizePiecePacket = await NetWorkStream.ReadAsync(bytes, 0, bytes.Length);
                    DataRecieved((bytes, sizePiecePacket));
                }
            }
            catch (Exception)
            {
            }
            
        }

        public async void SendAnswer(Answer answer)
        {
            var collection = Answer.GetTObjectPacketsBytes(answer);
            foreach (var item in collection)
            {
                await Task.Delay(1);
                await NetWorkStream.WriteAsync(item, 0, item.Length);
            }
        }
    }
}