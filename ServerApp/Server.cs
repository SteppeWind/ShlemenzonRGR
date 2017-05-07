using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp
{
    class Server
    {
        public static string PathServerDirecotory = @"F:\учеба\3 курс\2ой семестр\Шлемензон_всё\ргр\ServerFiles";
        public string Host { get; private set; } = "127.0.0.1";
        public int Port { get; private set; } = 1488;

        private TcpListener ListenerConnections;

        public Server() { }

        public Server(string host, int port)
        {
            Host = host;
            Port = port;
            ListenerConnections = new TcpListener(IPAddress.Any, port);
            ListenerConnections.Start();
            StartListening();
        }

        public async void StartListening()
        {
            while (true)
            {
                var tcpClient = await ListenerConnections.AcceptTcpClientAsync();
                Client client = new Client(tcpClient);
            }
        }
    }

    class Client
    {
        private TcpClient TcpClient;
        private NetworkStream NetWorkStream;
        private int SizeBuffer = 1000000;
        

        public Client(TcpClient tcp)
        {
            TcpClient = tcp;
            NetWorkStream = tcp.GetStream();
            ReadStreamData();
        }

        public async void ReadStreamData()
        {
            try
            {
                while (true)
                {
                    byte[] bytes = new byte[SizeBuffer];
                    var realSize = await NetWorkStream.ReadAsync(bytes, 0, bytes.Length);
                    Array.Resize(ref bytes, realSize);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}