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
        public ICollection<VMNewsElement> ListElements { get; set; }
    }
}