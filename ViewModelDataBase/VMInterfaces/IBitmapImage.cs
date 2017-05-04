using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace ViewModelDataBase.VMInterfaces
{
    public interface IBitmapImage
    {
        BitmapImage Poster { get; set; }
    }
}