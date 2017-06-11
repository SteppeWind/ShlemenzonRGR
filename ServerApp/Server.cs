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
using System.Runtime.Serialization.Formatters;
using RequestServer;
using ServerApp.DataModel;
using Model.UserTypes;
using Model.PublicationTypes;

namespace ServerApp
{
    class Server
    {
        public static string PathServerDirecotory = @"F:\учеба\3 курс\2ой семестр\Шлемензон_всё\ргр\ServerFiles";
        public string Host { get; private set; } = ServerSettings.Host;
        public int Port { get; private set; } = ServerSettings.Port;
        public event Action<string> ExceptionRecived;
        private int SizeBuffer = Packet.SizePacket * 2;
        public static int Delay { get; private set; } = 1;

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
                try
                {
                    var tcpClient = await ListenerConnections.AcceptTcpClientAsync();
                    Client client = new Client(tcpClient);
                    client.RecievedRequest += Client_RecievedRequest;
                }
                catch (Exception ex)
                {
                    ExceptionRecived(ex.Message);
                }
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
            Answer answer = null;
            switch (Request.DataType)
            {
                case DataType.Publication:
                    answer = PublicationRequests(Request);
                    break;

                case DataType.User:
                    answer = UserRequests(Request);
                    break;

                case DataType.Actor:
                    break;

                case DataType.SmallPublication:
                    answer = BasePublicationRequest(Request);
                    break;

                case DataType.Comment:
                    answer = CommentRequests(Request);
                    break;
            }
            client.SendAnswer(answer);
        }

        /// <summary>
        /// Обрабатывает запрос о списке публикаций
        /// </summary>
        /// <param name="mainRequest"></param>
        private Answer BasePublicationRequest(MainRequest mainRequest)
        {
            Answer answer = new Answer() { TypeAnswer = DataType.SmallPublication };
            string json = string.Empty;
            if (mainRequest.RecievedRequest != null)
                json = mainRequest.RecievedRequest.ToString();

            switch (mainRequest.TypeRequest)
            {
                case TypeRequest.Read:
                    var readPublicationRequest = JsonConvert.DeserializeObject<ReadPublciationRequest>(json);
                    answer.SelfAnswer = PublicationCRUD.GetSmallPublications(readPublicationRequest, mainRequest.UserId);
                    break;
                case TypeRequest.ReadSelf:
                    answer.SelfAnswer = PublicationCRUD.GetCertainPublications(mainRequest.UserId);
                    break;
            }
            return answer;
        }


        /// <summary>
        /// Обработка создания, удаления, обновления, чтения комментариев
        /// </summary>
        /// <param name="mainRequest"></param>
        private Answer CommentRequests(MainRequest mainRequest)
        {
            object result = null;
            Answer answer = new Answer();
            string json = string.Empty;
            if (mainRequest.RecievedRequest!= null)
            {
                json = mainRequest.ToString();
            }

            switch (mainRequest.TypeRequest)
            {
                case TypeRequest.Update:
                    result = CommentCRUD.UpdateComment(JsonConvert.DeserializeObject<Comment>(json));
                    break;

                case TypeRequest.Delete:
                    result = CommentCRUD.DeleteComment(int.Parse(json), mainRequest.UserId);
                    break;

                case TypeRequest.Create:
                    result = CommentCRUD.AddComment(JsonConvert.DeserializeObject<Comment>(json));
                    break;

                case TypeRequest.Read:
                    result = CommentCRUD.GetComments(int.Parse(json));
                    break;

                case TypeRequest.ReadSelf:
                    result = CommentCRUD.GetUserComments(mainRequest.UserId);
                    break;
            }
            answer.SelfAnswer = result;
            return answer;
        }


        /// <summary>
        /// Обработка запроса, поступившая от админа
        /// </summary>
        /// <param name="mainRequest">Запрос с данными</param>
        private Answer UserRequests(MainRequest mainRequest)
        {
            Answer answer = new Answer();
            string json = string.Empty;
            if (mainRequest.RecievedRequest != null)
                json = mainRequest.ToString();

            switch (mainRequest.TypeRequest)
            {
                case TypeRequest.Update:
                    var user = JsonConvert.DeserializeObject<User>(json);
                    answer.SelfAnswer = UserCRUD.UpdateUser(user);
                    break;

                case TypeRequest.Delete:
                    answer.SelfAnswer = UserCRUD.BanUser(int.Parse(json), mainRequest.UserId);
                    break;

                case TypeRequest.Undelete:
                    answer.SelfAnswer = UserCRUD.UnbanUser(int.Parse(json), mainRequest.UserId);
                    break;

                case TypeRequest.Create:
                    user = JsonConvert.DeserializeObject<User>(json);
                    answer.SelfAnswer = UserCRUD.CreateUser(user);
                    break;
                case TypeRequest.Read:
                    answer.SelfAnswer = UserCRUD.GetUsers;
                    break;
                case TypeRequest.ReadSelf:
                    string[] login_password = json.Split('%');
                    answer.SelfAnswer = UserCRUD.Authorization(login_password[0], login_password[1]);
                    break;
                default:
                    break;
            }

            return answer;
        }


        /// <summary>
        /// Обработка создания, удаления, обновления, чтения публикаций
        /// </summary>
        /// <param name="mainRequest"></param>
        private Answer PublicationRequests(MainRequest mainRequest)
        {
            object result = null;
            Answer answer = new Answer();
            string json = mainRequest.ToString();
            VMPublication castPublic = new VMPublication();
            if (mainRequest.TypeRequest == TypeRequest.Create || mainRequest.TypeRequest == TypeRequest.Update)
            {
                var @public = JsonConvert.DeserializeObject<VMPublication>(json);
                if (@public != null)
                {
                    switch (@public.TypePublication)
                    {
                        case Model.PublicationTypes.PublicationType.Game:
                            castPublic = JsonConvert.DeserializeObject<VMGamePublication>(json, new JsonSerializerSettings()
                            {
                                TypeNameHandling = TypeNameHandling.Auto
                            });
                            break;
                        case Model.PublicationTypes.PublicationType.Film:
                            castPublic = JsonConvert.DeserializeObject<VMFilmPublication>(json, new JsonSerializerSettings()
                            {
                                TypeNameHandling = TypeNameHandling.Auto
                            });
                            break;
                        case Model.PublicationTypes.PublicationType.Music:
                            castPublic = JsonConvert.DeserializeObject<VMMusicPublication>(json, new JsonSerializerSettings()
                            {
                                TypeNameHandling = TypeNameHandling.Auto
                            });
                            break;
                        case Model.PublicationTypes.PublicationType.News:
                            castPublic = JsonConvert.DeserializeObject<VMNewsPublication>(json, new JsonSerializerSettings()
                            {
                                TypeNameHandling = TypeNameHandling.Auto
                            });
                            break;
                        default:
                            break;
                    }
                }
            }

            switch (mainRequest.TypeRequest)
            {
                case TypeRequest.Update:
                    result = PublicationCRUD.UpdatePublication(castPublic, mainRequest.UserId);
                    break;
                case TypeRequest.Delete:
                    result = PublicationCRUD.DeletePublication(int.Parse(mainRequest.ToString()), mainRequest.UserId);
                    break;

                case TypeRequest.Undelete:
                    result = PublicationCRUD.UndeletePublication(int.Parse(mainRequest.ToString()), mainRequest.UserId);
                    break;
                case TypeRequest.Create:
                    result = PublicationCRUD.CreatePublication(castPublic);
                    break;
                case TypeRequest.Read:
                    result = PublicationCRUD.GetPublication(int.Parse(mainRequest.ToString()));
                    break;
            }
            answer.SelfAnswer = result;
            return answer;
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
        public event Action<int, int> NumPacketRecieved;
        private event Action<(byte[] bytes, int size)> DataRecieved;

        public Client(TcpClient tcp)
        {
            TcpClient = tcp;
            NetWorkStream = tcp.GetStream();
            ReadStreamDataAsync();
            DataRecieved += Client_DataRecieved;
            NumPacketRecieved += Client_NumPacketRecieved;
        }

        private void Client_NumPacketRecieved(int arg1, int arg2)
        {
            Console.WriteLine($"{arg1} пакет из {arg2}");
        }

        List<Packet> listPackets = new List<Packet>();
        private async void Client_DataRecieved((byte[] bytes, int size) item)
        {
            try
            {
                var packet = await Packet.GetPacketFromFixBytesAsync(item.bytes, item.size);
                lock (listPackets)
                {
                    if (packet != null)
                    {
                        //NumPacketRecieved(packet.NumberPacket, packet.TotalCountPackets);
                        listPackets.Add(packet);
                        if (listPackets.Count == packet.TotalCountPackets)
                        {
                            var res = MainRequest.GetTObjFromPackets(listPackets.OrderBy(el => el.NumberPacket).ToList());
                            listPackets.Clear();
                            RecievedRequest(this, res);
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
                await Task.Delay(Server.Delay);
                await NetWorkStream.WriteAsync(item, 0, item.Length);
            }
        }
    }
}