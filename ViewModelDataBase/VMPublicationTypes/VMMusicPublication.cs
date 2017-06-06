using Model.PublicationTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelDataBase.VMInterfaces;
using System.Collections.ObjectModel;
using ViewModelDataBase.VMTypes;
using Model;
using Newtonsoft.Json;

namespace ViewModelDataBase.VMPublicationTypes
{
    public class VMMusicPublication : VMPublication, IMusicPublication, IListSongs
    {
        [Property("Formats")]
        public string Formats { get; set; }

        [Property("Performer")]
        public string Performer { get; set; }

        [Property("CountryPerformer")]
        public string CountryPerformer { get; set; }

        [Property("Album")]
        public string Album { get; set; }

        [Property("ReleaseYear")]
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
                                 let currType = f.Type
                                 where FilterTypes.FiltersImage
                                 .Contains(currType.First() == '.' ? currType.Substring(1) : currType)
                                 select f as VMFile;
                }
                return listImages.ToList();
            }
        }

        private IEnumerable<VMFile> listSongs;
        [JsonIgnore]
        public List<VMFile> ListSongs
        {
            get
            {
                if (listSongs == null)
                {
                    listSongs = from f in ListFiles
                                where FilterTypes.FiltersMusic
                                .Contains(f.Type.First() == '.' ? f.Type.Substring(1) : f.Type)
                                select f as VMFile;
                }
                return listSongs.ToList();
            }
        }

        public VMMusicPublication()
        {
            TypePublication = PublicationType.Music;
        }
    }
}