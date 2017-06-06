using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.PublicationTypes
{
    public interface IListFiles<TFile>
        where TFile : IInfoFile
    {
        List<TFile> ListFiles { get; set; }
    }
}