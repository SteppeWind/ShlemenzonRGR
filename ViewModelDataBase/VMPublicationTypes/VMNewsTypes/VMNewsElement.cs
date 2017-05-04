using Model.PublicationTypes.NewsPublications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelDataBase.VMPublicationTypes.VMNewsTypes
{
    public class VMNewsElement : NewsElement
    {
        public VMPublication Publication { get; set; }
    }
}