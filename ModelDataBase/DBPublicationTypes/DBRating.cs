using Model.PublicationTypes;
using ModelDataBase.DBInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelDataBase.DBUserTypes;

namespace ModelDataBase.DBPublicationTypes
{
    public class DBRating : Rating, IUserDB, IPublicationDB
    {
        private int userId = -1;
        public int UserId
        {
            get => userId;
            set
            {
                if (userId == -1)
                    userId = value;
            }
        }

        private int publicationId = -1;
        public int PublicatoinId
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