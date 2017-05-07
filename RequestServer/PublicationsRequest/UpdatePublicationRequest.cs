﻿using RequestServer.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelDataBase.VMPublicationTypes;

namespace RequestServer.PublicationsRequest
{
    public class UpdatePublicationRequest : MainRequest
    {
        public VMPublication OldPublication { get; set; }

        public VMPublication NewPublication { get; set; }

    }
}