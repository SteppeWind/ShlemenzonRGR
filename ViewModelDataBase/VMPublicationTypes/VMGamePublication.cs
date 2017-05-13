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
    public class VMGamePublication : VMPublication, IGamePublication<IGenre>, IListBitmapImages
    {
        public string CompanyDeveloper { get; set; }

        public string InterfaceLanguage { get; set; }

        public string Platform { get; set; }

        public bool MultiPlayer { get; set; }

        public DateTime? ReleaseYear { get; set; }

        public virtual ICollection<IGenre> ListGenres { get; set; }

        public ObservableCollection<VMImage> ListImages { get; set; }

        public VMGamePublication()
        {
            TypePublication = PublicationType.Game;
            ListGenres = new List<IGenre>();
            ListImages = new ObservableCollection<VMImage>();
        }
    }
}