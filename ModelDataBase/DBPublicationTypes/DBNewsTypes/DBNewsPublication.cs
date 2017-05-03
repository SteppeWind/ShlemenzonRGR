using Model.PublicationTypes.NewsPublications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDataBase.DBPublicationTypes.DBNewsTypes
{
    class DBNewsPublication : DBPublication, INewsPublication<DBNewsElementFile>
    {
        public virtual ICollection<DBNewsElementFile> ListElements { get; set; }
    }
}