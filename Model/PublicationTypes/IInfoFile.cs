using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.PublicationTypes
{
    public interface IInfoFile
    {
        string FullPath { get; set; }

        string Name { get; set; }

        string Type { get; set; }
    }
}