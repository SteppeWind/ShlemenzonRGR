using Model.UserTypes;
using ModelDataBase.DBPublicationTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDataBase.DBUserTypes
{
    public class DBUser : User
    {
        private int userId = -1;
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId
        {
            get => userId;
            set
            {
                if (userId == -1)
                    userId = value;
            }
        }

        public virtual ICollection<DBRating> ListMarks { get; set; }

        public virtual ICollection<DBComment> ListComments { get; set; }

        public virtual ICollection<DBPublication> ListPublications { get; set; }

       
    }
}