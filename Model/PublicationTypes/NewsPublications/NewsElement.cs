using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.PublicationTypes.NewsPublications
{
    public class NewsElement
    {
        public virtual TypeElementOfNews TypeElement { get; set; }

        public virtual int NumberOfList { get; set; }
    }
}