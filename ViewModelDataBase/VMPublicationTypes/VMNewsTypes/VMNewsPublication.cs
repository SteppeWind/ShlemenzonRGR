using Model.PublicationTypes.NewsPublications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelDataBase.VMPublicationTypes.VMNewsTypes
{
    public class VMNewsPublication : VMPublication, INewsPublication<VMNewsElement>
    {
        public List<VMNewsElement> ListElements { get; set; }

        public VMNewsPublication()
        {
            TypePublication = Model.PublicationTypes.PublicationType.News;
        }
    }
}