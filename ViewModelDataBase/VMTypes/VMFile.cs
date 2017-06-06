using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelDataBase.VMInterfaces;

namespace ViewModelDataBase.VMTypes
{
    public class VMFile : IFileVM
    {
        public byte[] Bytes { get ; set ; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string FullPath { get ; set ; }
    }
}