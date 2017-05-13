using Model.PublicationTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDataBase.DBPublicationTypes
{
    [Table("DBMusicPublications")]
    public class DBMusicPublication : DBPublication, IMusicPublication<DBInfoFile, DBGenre>
    {
        public virtual string Formats { get; set; }

        public virtual string Performer { get; set; }

        public virtual string CountryPerformer { get; set; }

        public virtual string Album { get; set; }

        public virtual ICollection<DBInfoFile> ListSongs { get; set; }

        public virtual DateTime? ReleaseYear { get; set; }

        public ICollection<DBGenre> ListGenres { get; set ; }
    }
}