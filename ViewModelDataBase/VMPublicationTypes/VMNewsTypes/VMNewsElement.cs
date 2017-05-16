using Model.PublicationTypes.NewsPublications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelDataBase.VMPublicationTypes.VMNewsTypes
{
    public class VMNewsElement : INewsElement
    {
        public VMPublication Publication { get; set; }

        public TypeElementOfNews TypeElement { get; set; }

        public int NumberOfList { get; set; }
    }
}