using Model.PublicationTypes;
using ModelDataBase.DBInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDataBase.DBPublicationTypes
{
    class DBGamePubliaction : DBPublication, IGamePublication, IListFiles
    {
        public virtual string CompanyDeveloper { get ; set ; }

        public virtual string InterfaceLanguage { get; set; }

        public virtual string Platform { get; set; }

        public virtual bool MultiPlayer { get; set; }

        public virtual DateTime ReleaseYear { get; set; }

        public virtual string Genre { get; set; }

        /// <summary>
        /// Список фотографий к публикации
        /// </summary>
        public virtual ICollection<DBInfoFile> ListFiles { get; set; }
    }
}