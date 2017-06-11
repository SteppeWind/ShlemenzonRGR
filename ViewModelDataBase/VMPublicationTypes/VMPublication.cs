using Model.PublicationTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelDataBase.VMInterfaces;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Text;
using System.Collections.ObjectModel;
using ViewModelDataBase.VMTypes;
using Model.UserTypes;
using Newtonsoft.Json;

namespace ViewModelDataBase.VMPublicationTypes
{
    public class VMPublication : VMSmallPublication, IPublication<VMFile, Genre, Rating, VMComment>
    {
        [JsonIgnore]
        public override SmallPublication PublicationComponent
        {
            get => base.PublicationComponent;
            set
            {
                base.PublicationComponent = value;
            }
        }
        
        public VMFile Description { get; set; }

        public virtual List<VMFile> ListFiles { get; set; }

        //private List<Genre> listGenres;
        public List<Genre> ListGenres { get; set; }
        //public List<Genre> ListGenres
        //{
        //    get => listGenres.OrderByDescending(g => g.Name.Length).ToList();
        //    set
        //    {
        //        listGenres = value;
        //    }
        //}

        [JsonIgnore]
        public List<IName> ListNamesGenres { get => ListGenres.ToList<IName>(); }

        public List<Rating> ListMarks { get; set; }

        public List<VMComment> ListComments { get; set; }

        public VMPublication()
        {
            ListFiles = ListFiles ?? new List<VMFile>();
            ListGenres = ListGenres ?? new List<Genre>();
            ListMarks = ListMarks ?? new List<Rating>();
            ListComments = ListComments ?? new List<VMComment>();
        }
    }
}