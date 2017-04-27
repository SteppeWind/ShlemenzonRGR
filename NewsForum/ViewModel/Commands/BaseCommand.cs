using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;

namespace NewsForum.ViewModel.Commands
{
    class BaseCommand : DependencyObject, ICommand 
    {
        public event EventHandler CanExecuteChanged;

        public virtual bool CanExecute(object parameter)
        {
            return false;
        }

        public virtual void Execute(object parameter)
        {

        }
    }
}