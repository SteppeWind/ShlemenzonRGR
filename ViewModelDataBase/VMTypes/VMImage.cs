using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelDataBase.VMInterfaces;
using Windows.UI.Xaml.Media.Imaging;

namespace ViewModelDataBase.VMTypes
{
    public class VMImage : IFileVM
    {
        public string Type { get; set; }
        public byte[] Bytes { get; set; }
    }
}