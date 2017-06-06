using Model.PublicationTypes;
using ModelDataBase.DBInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelDataBase.DBUserTypes;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ModelDataBase.DBPublicationTypes
{
    public class DBRating : Rating, IUserDB
    {

        private int id = -1;
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get { return id; }
            set
            {
                if (id == -1)
                    id = value;
            }
        }



        private int userId = -1;
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

        private int publicationId = -1;
        [ForeignKey("Publication")]
        public override int PublicationId
        {
            get => publicationId;
            set
            {
                if (publicationId == -1)
                    publicationId = value;
            }
        }


        public DBUser User { get; set; }

        public DBPublication Publication { get; set; }
    }
}