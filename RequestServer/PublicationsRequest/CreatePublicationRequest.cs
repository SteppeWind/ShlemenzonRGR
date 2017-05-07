using RequestServer.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelDataBase.VMPublicationTypes;

namespace RequestServer.PublicationsRequest
{
    public class CreatePublicationRequest : MainRequest
    {
        public VMPublication NewPublication { get; set; }
    }
}