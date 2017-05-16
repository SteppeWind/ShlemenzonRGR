using Model.PublicationTypes.NewsPublications;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDataBase.DBPublicationTypes.DBNewsTypes
{
    [Table("DBNewsPublications")]
    public class DBNewsPublication : DBPublication, INewsPublication<DBNewsElement>
    {
        public List<DBNewsElement> ListElements { get; set; }

        public DBNewsPublication()
        {
            TypePublication = Model.PublicationTypes.PublicationType.News;
        }
    }
}