using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp
{
    class Server
    {
        int port = 1488;
        TcpListener listener;

        public Server()
        {
            start();
        }

        void start()
        {
            listener = new TcpListener(new IPEndPoint(IPAddress.Any, port));
            listener.Start();
            listen();
        }

        async void listen()
        {
            while (true)
            {
                var client = await listener.AcceptTcpClientAsync();
                Client newCLient = new Client(client);
            }
        }
    }

    class user
    {
        public string name;
        public string message;
        public byte[] хуйня;

        public static byte[] Serialize(user message)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, message);
            return stream.ToArray();
        }

        public static user DeSerialize(string stroka)
        {
            var otvet = JsonConvert.DeserializeObject<user>(stroka);
            return otvet;
        }
    }

    class Client
    {
        TcpClient tcpClient;
        NetworkStream stream;

        public Client(TcpClient client)
        {
            tcpClient = client;
            stream = client.GetStream();
            listenStream();
        }


        async void listenStream()
        {
            while (true)
            {
                byte[] bytes = new byte[1488];
                var data = await stream.ReadAsync(bytes, 0, bytes.Length);
                Array.Resize(ref bytes, data);
                var load = user.DeSerialize(Encoding.UTF8.GetString(bytes));
            }
        }
    }
}