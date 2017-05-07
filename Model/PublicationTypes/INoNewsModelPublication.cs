using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.PublicationTypes
{
    public interface INoNewsModelPublication<T> where T : IGenre
    {
        DateTime? ReleaseYear { get; set; }
        ICollection<T> ListGenres { get; set; }
        //ICollection<InfoFile> ListFiles { get; set; }
    }
}