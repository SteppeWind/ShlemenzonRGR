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
    [Table("DBMusicPublications")]
    public class DBMusicPublication : DBPublication, IMusicPublication
    {
        public string Formats { get; set; }

        public string Performer { get; set; }

        public string CountryPerformer { get; set; }

        public string Album { get; set; }

        //[NotMapped]
        //public virtual List<DBInfoFile> ListSongs { get; set; }

        public DateTime? ReleaseYear { get; set; }

        public DBMusicPublication()
        {
            TypePublication = PublicationType.Music;
        }
    }
}