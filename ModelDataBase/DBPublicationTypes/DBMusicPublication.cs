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
    [Table("DBMusicPublications")]
    public class DBMusicPublication : DBPublication, IMusicPublication
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

        public DBMusicPublication()
        {
            TypePublication = PublicationType.Music;
        }
    }
}