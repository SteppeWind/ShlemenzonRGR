using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.PublicationTypes.NewsPublications
{
    public interface INewsPublication<TNewsElement> where TNewsElement : INewsElement
    {
        List<TNewsElement> ListElements { get; set; }
    }
}