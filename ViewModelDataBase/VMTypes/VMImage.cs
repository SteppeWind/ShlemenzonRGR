using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelDataBase.VMInterfaces;
using Windows.UI.Xaml.Media.Imaging;

namespace ViewModelDataBase.VMTypes
{
    public class VMImage : IBitmapImage
    {
        public BitmapImage Poster { get; set; }
    }
}