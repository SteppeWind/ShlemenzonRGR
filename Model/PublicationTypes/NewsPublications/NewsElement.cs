using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.PublicationTypes.NewsPublications
{
    public class NewsElement : INewsElement
    {
        public virtual int PublicationId { get; set; }

        public TypeElementOfNews TypeElement { get; set; } = TypeElementOfNews.Separator;

        public int NumberOfList { get; set; }
    }
}