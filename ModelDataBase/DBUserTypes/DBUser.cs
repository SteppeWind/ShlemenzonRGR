using Model;
using Model.PublicationTypes;
using Model.UserTypes;
using ModelDataBase.DBPublicationTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ModelDataBase.DBUserTypes
{
    public class DBUser : User, IUser<DBPublication, DBRating, DBComment>
    {
        private int userId = -1;
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int UserId
        {
            get => userId;
            set
            {
                if (userId == -1)
                    userId = value;
            }
        }

        //[NotMapped]
        //public override User UserComponent { get => base.UserComponent; set => base.UserComponent = value; }

        [NotMapped]
        public override bool IsCorrectUserForAutorize => base.IsCorrectUserForAutorize;

        public virtual List<DBPublication> ListPublications { get; set; }

        public virtual List<DBRating> ListRatings { get; set; }

        public virtual List<DBComment> ListComments { get; set; }
        
        public DBUser()
        {
            ListPublications = ListPublications ?? new List<DBPublication>();
            ListRatings = ListRatings ?? new List<DBRating>();
            ListComments = ListComments ?? new List<DBComment>();
        }
    }
}