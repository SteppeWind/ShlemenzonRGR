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
        public DataType TypeAnswer { get; set; } = DataType.Bool;

        public Object SelfAnswer { get; set; }        
    }
}