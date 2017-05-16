using Model.PublicationTypes;
using ModelDataBase.DBInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDataBase.DBPublicationTypes
{
    [Table("DBGamePubliactions")]
    public class DBGamePubliaction : DBPublication, IGamePublication
    {
        public virtual string CompanyDeveloper { get ; set ; }

        public virtual string InterfaceLanguage { get; set; }

        public virtual string Platform { get; set; }

        public virtual bool MultiPlayer { get; set; }

        public virtual DateTime? ReleaseYear { get; set; }      
    }
}