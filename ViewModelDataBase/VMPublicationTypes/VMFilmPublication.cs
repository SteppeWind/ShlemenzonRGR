using Model.PublicationTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelDataBase.VMInterfaces;

namespace ViewModelDataBase.VMPublicationTypes
{
    public class VMFilmPublication : VMPublication, IListBitmapImages, IFilmPublication<VMActor>
    {
        public ObservableCollection<IBitmapImage> ListImages { get; set; }

        public string Country { get; set; }

        public string Duration { get; set; }

        public string Director { get; set; }

        public ICollection<VMActor> ListActors { get; set; }

        public DateTime ReleaseYear { get; set; }

        public string Genre { get; set; }
    }
}