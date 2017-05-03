using Model.PublicationTypes;
using ModelDataBase.DBInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelDataBase.DBUserTypes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelDataBase.DBPublicationTypes
{
    public class DBComment : Comment, IUserDB, ICommentDB, IPublicationDB
    {
        private int commentId = -1;
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentId
        {
            get => commentId;
            set
            {
                if (commentId == -1)
                    commentId = value;
            }
        }

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

        public DBPublication Publication { get ; set; }
        
        public DBUser User { get; set; }
    }
}