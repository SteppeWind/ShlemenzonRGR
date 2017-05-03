using ModelDataBase.DBPublicationTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDataBase.DBInterfaces
{
    public interface IListFiles
    {
        ICollection<DBInfoFile> ListFiles { get; set; }
    }
}