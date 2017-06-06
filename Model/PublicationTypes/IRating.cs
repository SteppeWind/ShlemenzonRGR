using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.PublicationTypes
{
    public interface IRating
    {
        int UserId { get; set; }

        int PublicationId { get; set; }

        int Mark { get; set; }
    }
}