using Model.PublicationTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelDataBase.VMInterfaces;
using System.Collections.ObjectModel;
using ViewModelDataBase.VMTypes;

namespace ViewModelDataBase.VMPublicationTypes
{
    public class VMMusicPublication : VMPublication, IMusicPublication<VMSong, IGenre>, IListBitmapImages
    {
        public string Formats { get; set; }

        public string Performer { get; set; }

        public string CountryPerformer { get; set; }

        public string Album { get; set; }

        public ICollection<VMSong> ListSongs { get; set; }

        public DateTime? ReleaseYear { get; set; }

        public virtual ICollection<IGenre> ListGenres { get; set; }

        public ObservableCollection<VMImage> ListImages { get; set; }
    }
}