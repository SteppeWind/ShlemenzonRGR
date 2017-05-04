using Model.PublicationTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelDataBase.VMPublicationTypes
{
    public class VMRating : Rating
    {
        public VMUser User { get; set; }

        public VMPublication Publication { get; set; }
    }
}