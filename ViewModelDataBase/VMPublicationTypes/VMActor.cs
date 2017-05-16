using Model.PublicationTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelDataBase.VMPublicationTypes
{
    public class VMActor : IActor
    {
        public ObservableCollection<VMFilmPublication> ListFilms { get; set; }
        public string Name { get; set; }
    }
}