using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace NewsForum.Model
{
    class ImageContainer : INotifyPropertyChanged, IFileSettings
    {
        private string fullPath;
        public String FullPath
        {
            get => fullPath;
            set
            {
                fullPath = value;
                ChangeProperty();
            }
        }

        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                ChangeProperty();
            }
        }

        private BitmapImage bitmap;
        public BitmapImage BitMapImg
        {
            get => bitmap;
            set
            {
                bitmap = value;
                ChangeProperty();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void ChangeProperty([CallerMemberName] string propertyName="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
