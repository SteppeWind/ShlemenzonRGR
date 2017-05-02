using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Text;

namespace ServerApp
{
    class Program
    {      
        static void Main(string[] args)
        {
            var server = new Server();
            Console.ReadKey();
        }
    }
}
