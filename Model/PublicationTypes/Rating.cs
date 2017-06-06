using Model.UserTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.PublicationTypes
{
    public class Rating : ConvertProperty, IRating
    {
        //public virtual User User { get; set; }

        //public virtual Publication Publication { get; set; }

        [Property("UserId")]
        public virtual int UserId { get; set; }

        [Property("PublicationId")]
        public virtual int PublicationId { get; set; }

        [Property("Mark")]
        public virtual int Mark { get; set; }
    }
}