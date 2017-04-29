using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Publications
{
    class FilmPublication : Publication
    {
        private DateTime date;
        /// <summary>
        /// Год выпуска фильма 
        /// </summary>
        public DateTime ReleaseYear
        {
            get { return date; }
            set
            {
                date = value;
                ChangeProperty();
            }
        }


        private string country;
        public string Country
        {
            get { return country; }
            set
            {
                country = value;
                ChangeProperty();
            }
        }


        private string duration;
        public string Duration
        {
            get { return duration; }
            set
            {
                duration = value;
                ChangeProperty();
            }
        }

        private string translation;
        public string Ttranslation
        {
            get { return translation; }
            set
            {
                translation = value;
                ChangeProperty();
            }
        }

        private string director;
        /// <summary>
        /// Режиссер
        /// </summary>
        public string Director
        {
            get { return director; }
            set
            {
                director = value;
                ChangeProperty();
            }
        }

        private string[] actors;
        /// <summary>
        /// Актеры фильма
        /// </summary>
        public string[] Actors
        {
            get { return actors; }
            set
            {
                actors = value;
                ChangeProperty();
            }
        }

    }
}