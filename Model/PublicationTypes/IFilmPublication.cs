using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.PublicationTypes
{
    public interface IFilmPublication<T> where T : Actor
    {
        string Country { get; set; }

        string Duration { get; set; }
        
        /// <summary>
        /// Режиссер
        /// </summary>
        string Director { get; set; }

        List<T> ListActors { get; set; }

        DateTime? ReleaseYear { get; set; }

        string LinkTrailer { get; set; }
    }
}