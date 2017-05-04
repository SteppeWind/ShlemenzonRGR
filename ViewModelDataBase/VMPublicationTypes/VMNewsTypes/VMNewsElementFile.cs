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
    public class VMNewsElementFile : VMNewsElement, IStorageFIleVM
    {
        public StorageFile File { get; set; }

        public string Type { get ; set ; }

        public string Name { get; set; }       
    }
}