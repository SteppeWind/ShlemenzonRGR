using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelDataBase.VMInterfaces
{
    public interface IModifyPropertyChanged : INotifyPropertyChanged
    {
        void ChangeProp([CallerMemberName] string name = "");
    }
}