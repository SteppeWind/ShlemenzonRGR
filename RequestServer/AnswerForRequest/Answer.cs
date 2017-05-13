using Newtonsoft.Json;
using RequestServer.PacketRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestServer.AnswerForRequest
{
    public class Answer : SendReciveObject<Answer>
    {
        public TypeAnswer TypeAnswer { get; set; }

        public DataType DataType { get; set; }

        public Object SelfAnswer { get; set; }        
    }
}