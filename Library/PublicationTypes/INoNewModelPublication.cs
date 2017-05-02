using ModelDataBase.FileTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDataBase.PublicationTypes
{
    public interface INoNewModelPublication
    {
        DateTime ReleaseYear { get; set; }
        string Genre { get; set; }

        ICollection<ModelInfoFile> ListFiles { get; set; }
    }
}