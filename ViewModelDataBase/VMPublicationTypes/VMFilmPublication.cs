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
    public class VMFilmPublication : VMPublication, IFilmPublication<Actor>, IListBitmapImages
    {
        [Property("Country")]
        public string Country { get; set; }

        [Property("Duration")]
        public string Duration { get; set; }

        [Property("Director")]
        public string Director { get; set; }

        //private List<Actor> listActors;
        public List<Actor> ListActors { get; set; }
        //public List<Actor> ListActors
        //{
        //    get => listActors?.OrderByDescending(a => a.Name.Length).ToList();
        //    set
        //    {
        //        listActors = value;
        //    }
        //}

        [JsonIgnore]
        public List<IName> ListNamesActors { get => ListActors.ToList<IName>(); }

        [Property("ReleaseYear")]
        public DateTime? ReleaseYear { get; set; }


        private VMNewsTypes.VMElementLinkVideo linkVideo;
        [JsonIgnore]
        public VMNewsTypes.VMElementLinkVideo LinkVideo
        {
            get
            {
                if (linkVideo == null)
                {
                    linkVideo = new VMNewsTypes.VMElementLinkVideo();
                }

                if (LinkTrailer != null)
                {
                    linkVideo.SetResultCode(LinkTrailer);
                }

                return linkVideo;
            }
        }

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

        [Property("LinkTrailer")]
        public string LinkTrailer { get; set; }

        public VMFilmPublication()
        {
            ListActors = ListActors ?? new List<Actor>();
            TypePublication = PublicationType.Film;
        }
    }
}