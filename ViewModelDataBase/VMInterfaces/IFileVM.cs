﻿using Model.PublicationTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelDataBase.VMInterfaces
{
    public interface IFileVM : IInfoFile
    {
        byte[] Bytes { get; set; }
    }
}