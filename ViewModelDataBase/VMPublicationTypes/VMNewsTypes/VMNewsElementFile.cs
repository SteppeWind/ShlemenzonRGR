using Model.PublicationTypes;
using Model.PublicationTypes.NewsPublications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelDataBase.VMInterfaces;
using Windows.Storage;

namespace ViewModelDataBase.VMPublicationTypes.VMNewsTypes
{
    public class VMNewsElementFile : NewsElement, IFileVM
    {
        public string Name { get; set; }

        public byte[] Bytes { get; set; }

        public string Type { get; set ; }

        public string FullPath { get; set; }
    }
}