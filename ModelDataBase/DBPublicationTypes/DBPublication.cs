using Model.PublicationTypes;
using ModelDataBase.DBInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelDataBase.DBUserTypes;

namespace ModelDataBase.DBPublicationTypes
{
    public class DBPublication : Publication<DBInfoFile, DBGenre>, IPosterDB, IDescriptionDB, IUserDB
    {
        private int publicationId = -1;
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PublicatoinId
        {
            get => publicationId;
            set
            {
                if (publicationId == -1)
                    publicationId = value;
            }
        }

        private int userId = -1;
        [ForeignKey("User")]
        public int UserId
        {
            get => userId;
            set
            {
                if (userId == -1)
                    userId = value;
            }
        }

        public virtual string RefPoster { get; set; }
        public virtual string RefDescription { get; set; }

        public virtual DBUser User { get; set; }

        public virtual ICollection<DBRating> ListMarks { get; set; }

        public virtual ICollection<DBComment> ListComments { get; set; }

        //public virtual List<DBGenre> ListGenres { get; set; }

        ///// <summary>
        ///// Список фотографий к публикации
        ///// </summary>
        //public virtual List<DBInfoFile> ListFiles { get; set; }
    }
}