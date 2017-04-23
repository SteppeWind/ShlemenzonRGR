using NewsForum.Model;
using NewsForum.Pages.EditorPublication;
using NewsForum.View.MyUserControls;
using NewsForum.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NewsForum.View.Commands
{
    //класс для удаления фото элемента с GridView (ThirdStepDistributionEditorPage)
    class DeleteImageFromFotoElementsCommand : ICommand
    {

        public PhotoElementsBaseViewModel ViewModelBase { get; set; }

        public DeleteImageFromFotoElementsCommand(PhotoElementsBaseViewModel viewModel)
        {
            ViewModelBase = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (parameter != null)
            {
                if (parameter is ImageContainer)
                    return true;

                return false;
            }
            return false;
        }

        public void Execute(object parameter)
        {
            ViewModelBase.RemoveElement((ImageContainer)parameter);
        }
    }
}
