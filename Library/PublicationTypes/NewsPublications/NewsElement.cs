using ModelDataBase.PublicationTypes;
using ModelDataBase.PublicationTypes.NewsPublications;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.PublicationTypes.NewsPublications
{
    public class NewsElement
    {     
        public virtual TypeElementOfNews TypeElement { get; set; }
        
        public virtual int NumberOfList { get; set; }
    }
}