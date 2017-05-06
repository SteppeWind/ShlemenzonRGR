using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.PublicationTypes
{
    public interface INoNewsModelPublication
    {
        DateTime? ReleaseYear { get; set; }
        string Genre { get; set; }

        //ICollection<InfoFile> ListFiles { get; set; }
    }
}