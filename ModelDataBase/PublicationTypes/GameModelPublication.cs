using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelDataBase.FileTypes;

namespace ModelDataBase.PublicationTypes
{
    class GameModelPublication : ModelPublication, INoNewModelPublication
    {

        /// <summary>
        /// Год выпуска игры
        /// </summary>
        public virtual DateTime ReleaseYear { get; set; }

        public virtual string Genre { get; set; }
        /// <summary>
        /// Разработчик
        /// </summary>
        public virtual string CompanyDeveloper { get; set; }

        /// <summary>
        /// Язык интерфейса
        /// </summary>
        public virtual string InterfaceLanguage { get; set; }

        /// <summary>
        /// Платформа игры
        /// </summary>
        public virtual string Platform { get; set; }
        
        /// <summary>
        /// Поддержка мультиплеера
        /// </summary>
        public virtual bool MultiPlayer { get; set; }

        public ICollection<ModelInfoFile> ListFiles { get; set; }
    }
}