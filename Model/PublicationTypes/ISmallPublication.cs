using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.PublicationTypes
{
    public interface ISmallPublication
    {
        int UserId { get; set; }

        int PublicationId { get; set; }

        string Title { get; set; }

        PublicationType TypePublication { get; set; }

        DateTime CreateDate { get; set; }

        bool IsDeleted { get; set; }

        bool IsPublished { get; set; }
    }
}