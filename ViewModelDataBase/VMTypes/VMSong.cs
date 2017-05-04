using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelDataBase.VMInterfaces;
using Windows.Storage;
using Windows.Storage.Streams;

namespace ViewModelDataBase.VMTypes
{
    public class VMSong : ISongFile
    {
        public IRandomAccessStream Stream { get; set; }

        public StorageFile File { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }
    }
}