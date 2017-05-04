using Model.PublicationTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelDataBase.VMInterfaces;
using System.Collections.ObjectModel;

namespace ViewModelDataBase.VMPublicationTypes
{
    class VMGamePublication : VMPublication, IGamePublication, IListBitmapImages
    {
        public string CompanyDeveloper { get; set; }

        public string InterfaceLanguage { get; set; }

        public string Platform { get; set; }

        public bool MultiPlayer { get; set; }

        public DateTime ReleaseYear { get; set; }

        public string Genre { get; set; }

        public ObservableCollection<IBitmapImage> ListImages { get; set; }
    }
}