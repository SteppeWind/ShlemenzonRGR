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
using Model.UserTypes;
using Model;

namespace ModelDataBase.DBPublicationTypes
{
    public class DBPublication :
        SmallPublication,
        IPublication<DBInfoFile, DBGenre, DBRating, DBComment>, 
        IPosterDB,
        IDescriptionDB
    {
        [NotMapped]
        public override SmallPublication PublicationComponent
        {
            get => base.PublicationComponent;
            set
            {
                base.PublicationComponent = value;
                
            }
        }
       
        private int publicationId = -1;
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Property("PublicationId")]
        public override int PublicationId
        {
            get => publicationId;
            set
            {
                if (publicationId == -1)
                    publicationId = value;
            }
        }

        private int userId = -1;

        [Property("UserId")]
        [ForeignKey("User")]
        public override int UserId
        {
            get => userId;
            set
            {
                if (userId == -1)
                    userId = value;
            }
        }

        private bool isDeleted = false;
        [Property("IsDeleted")]
        public override bool IsDeleted
        {
            get => isDeleted;
            set
            {
                isDeleted = value;
                IsPublished = !isDeleted;
            }
        }
        

        public virtual DBUser User { get; set; }

        public string RefPoster { get; set; }

        public string RefDescription { get; set; }

        public virtual List<DBInfoFile> ListFiles { get; set; }

        public virtual List<DBGenre> ListGenres { get; set; }

        public virtual List<DBRating> ListMarks { get; set; }

        public virtual List<DBComment> ListComments { get; set; }

        public DBPublication()
        {
            ListFiles = ListFiles ?? new List<DBInfoFile>();
            ListGenres = ListGenres ?? new List<DBGenre>();
            ListMarks = ListMarks ?? new List<DBRating>();
            ListComments = ListComments ?? new List<DBComment>();
        }
        
        //public virtual List<DBGenre> ListGenres { get; set; }

        ///// <summary>
        ///// Список фотографий к публикации
        ///// </summary>
        //public virtual List<DBInfoFile> ListFiles { get; set; }
    }
}