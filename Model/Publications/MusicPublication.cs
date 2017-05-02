using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Publications
{
    class MusicPublication : Publication
    {

        private DateTime date;
        /// <summary>
        /// Год выпуска песни или альбома 
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


        private string genre;
        public string Genre
        {
            get { return genre; }
            set
            {
                genre = value;
                ChangeProperty();
            }
        }

        private string formats;
        /// <summary>
        /// Форматы песни или альбома, (wav, mp3 и т.д.)
        /// </summary>
        public string Formats
        {
            get { return formats; }
            set
            {
                formats = value;
                ChangeProperty();
            }
        }

        private string performer;
        /// <summary>
        /// Исполнитель
        /// </summary>
        public string Performer
        {
            get { return performer; }
            set
            {
                performer = value;
                ChangeProperty();
            }
        }

        private string countryPerformer;
        /// <summary>
        /// Страна исполнителя
        /// </summary>
        public string CountryPerformer
        {
            get { return countryPerformer; }
            set
            {
                countryPerformer = value;
                ChangeProperty();
            }
        }


        private string album;
        /// <summary>
        /// Наименование альбома
        /// </summary>
        public string Album
        {
            get { return album; }
            set
            {
                album = value;
                ChangeProperty();
            }
        }
    }
}