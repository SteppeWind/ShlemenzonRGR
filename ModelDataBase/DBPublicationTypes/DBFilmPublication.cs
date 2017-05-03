using Model.PublicationTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDataBase.DBPublicationTypes
{
    public class DBFilmPublication : DBPublication, IFilmPublication<DBActor>
    {
        public virtual string Country { get; set; }

        public virtual string Duration { get; set; }

        public virtual string Director { get; set; }

        public virtual ICollection<DBActor> ListActors { get; set; }

        public virtual DateTime ReleaseYear { get; set; }

        public virtual string Genre { get; set; }
    }
}