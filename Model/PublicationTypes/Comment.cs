using Model.UserTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.PublicationTypes
{
    public class Comment : ConvertProperty, IComment
    {
        [Property("CommentId")]
        public virtual int CommentId { get; set; }

        [Property("UserId")]
        public virtual int UserId { get; set; }

        [Property("PublicationId")]
        public virtual int PublicationId { get; set; }

        /// <summary>
        /// Значение комментария 
        /// </summary>
        [Property("Value")]
        public string Value { get; set; }
    }
}