using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.PublicationTypes
{
    public interface IFilmPublication<T> : INoNewsModelPublication where T: class 
    {
        string Country { get; set; }

        string Duration { get; set; }
        
        /// <summary>
        /// Режиссер
        /// </summary>
        string Director { get; set; }

        ICollection<T> ListActors { get; set; }
    }
}