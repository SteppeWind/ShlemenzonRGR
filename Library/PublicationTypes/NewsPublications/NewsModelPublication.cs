using Library.PublicationTypes.NewsPublications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDataBase.PublicationTypes.NewsPublications
{
    public class NewsModelPublication : ModelPublication
    {
        public virtual ICollection<NewsElement> ListNewsElements { get; set; }
    }
}