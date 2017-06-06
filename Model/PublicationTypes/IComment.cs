using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.PublicationTypes
{
    public interface IComment
    {
        int CommentId { get; set; }

        int UserId { get; set; }

        int PublicationId { get; set; }

        /// <summary>
        /// Значение комментария 
        /// </summary>
        string Value { get; set; }
    }
}