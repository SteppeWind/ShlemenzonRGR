using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace ViewModelDataBase.VMInterfaces
{
    public interface IStorageFIleVM
    {
        StorageFile File { get; set; }

        string Type { get; set; }
   
        string Name { get; set; }
    }
}