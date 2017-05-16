using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.PublicationTypes.NewsPublications
{
    public interface INewsElement
    {
        TypeElementOfNews TypeElement { get; set; }

        int NumberOfList { get; set; }
    }
}