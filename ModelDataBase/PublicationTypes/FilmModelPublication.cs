using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelDataBase.FileTypes;

namespace ModelDataBase.PublicationTypes
{
    class FilmModelPublication : ModelPublication, INoNewModelPublication
    {
        /// <summary>
        /// Год выпуска фильма 
        /// </summary>
        public virtual DateTime ReleaseYear { get; set; }

        public virtual string Country { get; set; }

        public virtual string Duration { get; set; }

        public virtual string Genre { get; set; }

        /// <summary>
        /// Режиссер
        /// </summary>
        public virtual string Director { get; set; }

        /// <summary>
        /// Актеры фильма
        /// </summary>
        public virtual ICollection<Actor> Actors { get; set; }

        public ICollection<ModelInfoFile> ListFiles { get; set; }
    }
}