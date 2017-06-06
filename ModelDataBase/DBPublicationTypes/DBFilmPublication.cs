using Model;
using Model.PublicationTypes;
using ModelDataBase.DBInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDataBase.DBPublicationTypes
{
    [Table("DBFilmPublications")]
    public class DBFilmPublication : DBPublication, IFilmPublication<DBActor>
    {
        [Property("Country")]
        public string Country { get; set; }

        [Property("Duration")]
        public string Duration { get; set; }

        [Property("Director")]
        public string Director { get; set; }

        public virtual List<DBActor> ListActors { get; set; }

        [Property("ReleaseYear")]
        public DateTime? ReleaseYear { get; set; }

        [Property("LinkTrailer")]
        public string LinkTrailer { get; set; }

        public DBFilmPublication()
        {
            TypePublication = PublicationType.Film;
        }

    }
}