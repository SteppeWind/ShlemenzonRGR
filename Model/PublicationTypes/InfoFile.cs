using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.PublicationTypes
{
    public class InfoFile :IInfoFile
    {
        public virtual string Name { get; set; }

        //public virtual Publication Publication { get; set; }
    }
}