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
    public class VMMusicPublication : VMPublication, IMusicPublication<ISongFile, IGenre>, IListBitmapImages
    {
        public string Formats { get; set; }

        public string Performer { get; set; }

        public string CountryPerformer { get; set; }

        public string Album { get; set; }

        public ICollection<ISongFile> ListSongs { get; set; }

        public DateTime? ReleaseYear { get; set; }

        public virtual ICollection<IGenre> ListGenres { get; set; }

        public ObservableCollection<IBitmapImage> ListImages { get; set; }
    }
}