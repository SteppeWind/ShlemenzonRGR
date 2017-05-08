using Model.PublicationTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelDataBase.VMInterfaces;
using ViewModelDataBase.VMTypes;

namespace ViewModelDataBase.VMPublicationTypes
{
    public class VMFilmPublication : VMPublication, IListBitmapImages, IFilmPublication<VMActor, IGenre>
    {
        public ObservableCollection<VMImage> ListImages { get; set; }

        public string Country { get; set; }

        public string Duration { get; set; }

        public string Director { get; set; }

        public ICollection<VMActor> ListActors { get; set; }

        public DateTime? ReleaseYear { get; set; }

        public virtual ICollection<IGenre> ListGenres { get; set; }
    }
}