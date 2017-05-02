using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelDataBase.FileTypes;

namespace ModelDataBase.PublicationTypes
{
    class MusicModelPublication : ModelPublication, INoNewModelPublication
    {
        /// <summary>
        /// Год выпуска песни или альбома 
        /// </summary>
        public virtual DateTime ReleaseYear { get; set; }

        public virtual string Genre { get; set; }
        
        /// <summary>
        /// Форматы песни или альбома, (wav, mp3 и т.д.)
        /// </summary>
        public virtual string Formats { get; set; }
        
        /// <summary>
        /// Исполнитель
        /// </summary>
        public virtual string Performer { get; set; }
        
        /// <summary>
        /// Страна исполнителя
        /// </summary>
        public virtual string CountryPerformer { get; set; }

        /// <summary>
        /// Наименование альбома
        /// </summary>
        public virtual string Album { get; set; }

        /// <summary>
        /// Список фотографий
        /// </summary>
        public ICollection<ModelInfoFile> ListFiles { get; set; }

        /// <summary>
        /// Список песен публикации
        /// </summary>
        public ICollection<ModelInfoFile> ListAudios { get; set; }
    }
}