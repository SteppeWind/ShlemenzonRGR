using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace ViewModelDataBase.VMInterfaces
{
    public interface ISongFile : IStorageFIleVM
    {
        /// <summary>
        /// Аудио-поток
        /// </summary>
        IRandomAccessStream Stream { get; set; }
    }
}