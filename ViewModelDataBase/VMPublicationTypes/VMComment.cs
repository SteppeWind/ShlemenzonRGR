using Model.PublicationTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelDataBase.VMPublicationTypes
{
    public class VMComment : Comment
    {
        public VMUser User { get; set; }

        public VMPublication Publication { get; set; }
    }
}