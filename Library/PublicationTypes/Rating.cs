using ModelDataBase.UserTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDataBase.PublicationTypes
{
    public class Rating
    {
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
        
        public virtual User User { get; set; }


        private int publicationId = -1;
        [ForeignKey("Publication")]
        public int PublicationId
        {
            get { return publicationId; }
            set
            {
                if (publicationId == -1)
                    publicationId = value;
            }
        }
        
        public virtual ModelPublication Publication { get; set; }

        public virtual int Mark { get; set; }
    }
}
