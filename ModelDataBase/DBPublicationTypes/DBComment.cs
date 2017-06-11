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
using Model;

namespace ModelDataBase.DBPublicationTypes
{
    public class DBComment : Comment, IUserDB
    {
        private int commentId = -1;
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int CommentId
        {
            get => commentId;
            set
            {
                if (commentId == -1)
                    commentId = value;
            }
        }

        private int userId = -1;
        [ForeignKey("User")]
        [Property("UserId")]
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

        public DBPublication Publication { get ; set; }
        
        public virtual DBUser User { get; set; }
    }
}