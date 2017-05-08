using RequestServer.PublicationsRequest.SortingsDataRequest;
using RequestServer.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestServer.PublicationsRequest
{
    public class ReadPublciationRequest : MainRequest
    {
        public PublicationsSortingData SortingData { get; set; }
    }
}