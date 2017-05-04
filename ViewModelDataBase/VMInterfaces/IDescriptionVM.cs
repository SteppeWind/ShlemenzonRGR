using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Text;

namespace ViewModelDataBase.VMInterfaces
{
    public interface IDescriptionVM
    {
        ITextDocument Description { get; set; }
    }
}