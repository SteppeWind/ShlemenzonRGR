using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.PublicationTypes.NewsPublications
{
    public interface INewsPublication<T> where T : class
    {
        public ICollection<T> ListElements { get; set; }
    }
}