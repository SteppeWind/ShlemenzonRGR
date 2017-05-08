using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelDataBase.VMTypes;

namespace ViewModelDataBase.VMInterfaces
{
    public interface IListBitmapImages
    {
        ObservableCollection<VMImage> ListImages { get; set; }
    }
}
