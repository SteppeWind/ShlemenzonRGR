using Model;
using Model.PublicationTypes;
using Newtonsoft.Json;
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
    public class VMFilmPublication : VMPublication, IFilmPublication<VMActor>, IListBitmapImages
    {
        public string Country { get; set; }

        public string Duration { get; set; }

        public string Director { get; set; }

        public List<VMActor> ListActors { get; set; }

        public DateTime? ReleaseYear { get; set; }

        private IEnumerable<VMFile> listImages;
        [JsonIgnore]
        public List<VMFile> ListImages
        {
            get
            {
                if (listImages == null)
                {
                    listImages = from f in ListFiles
                                 where FilterTypes.FiltersImage
                                 .Contains(f.Type.First() == '.' ? f.Type.Substring(1) : f.Type)
                                 select f as VMFile;
                }
                return listImages.ToList();
            }
        }

        public VMFilmPublication()
        {
            ListActors = ListActors ?? new List<VMActor>();
            TypePublication = PublicationType.Film;
        }
    }
}