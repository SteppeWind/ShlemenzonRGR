using Model.PublicationTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDataBase.DBPublicationTypes
{
    [Table("DBFilmPublications")]
    public class DBFilmPublication : DBPublication, IFilmPublication<DBActor, DBGenre>
    {
        public virtual string Country { get; set; }

        public virtual string Duration { get; set; }

        public virtual string Director { get; set; }

        public virtual ICollection<DBActor> ListActors { get; set; }

        public virtual DateTime? ReleaseYear { get; set; }

        public ICollection<DBGenre> ListGenres { get; set ; }
    }
}