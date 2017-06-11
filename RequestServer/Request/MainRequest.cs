using Newtonsoft.Json;
using RequestServer.PacketRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestServer.Request
{
    public class MainRequest : SendReciveObject<MainRequest>
    {
        public TypeRequest TypeRequest { get; set; }
        
        public DataType DataType { get; set; }

        public int UserId { get; set; }

        public object RecievedRequest { get; set; }


        public override string ToString()
        {
            return RecievedRequest.ToString();
        }
    }
}