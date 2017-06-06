using Model;
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
        [Property("CompanyDeveloper")]
        public virtual string CompanyDeveloper { get; set; }

        [Property("InterfaceLanguage")]
        public virtual string InterfaceLanguage { get; set; }

        [Property("Platform")]
        public virtual string Platform { get; set; }

        [Property("MultiPlayer")]
        public virtual bool MultiPlayer { get; set; }

        [Property("ReleaseYear")]
        public virtual DateTime? ReleaseYear { get; set; }
    }
}