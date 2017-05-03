using Model.UserTypes;
using ModelDataBase.DBPublicationTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDataBase.DBUserTypes
{
    public class DBUser : User
    {
        public virtual ICollection<DBRating> ListMarks { get; set; }

        public virtual ICollection<DBComment> ListComments { get; set; }

        public virtual ICollection<DBPublication> ListPublications { get; set; }
    }
}
