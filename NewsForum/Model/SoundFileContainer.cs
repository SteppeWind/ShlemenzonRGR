using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace NewsForum.Model
{
    class SoundFileContainer : IFileSettings, INotifyPropertyChanged
    {
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

        private string fullPath;
        public string FullPath
        {
            get => fullPath;
            set
            {
                fullPath = value;
                ChangeProperty();
            }
        }


        public IRandomAccessStream AccessStream { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void ChangeProperty([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
