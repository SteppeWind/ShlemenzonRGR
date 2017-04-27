using NewsForum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace NewsForum.ViewModel.Commands
{
    class AddDeleteCommand : BaseCommand
    {

        public AddDeleteCommand(BaseCollectionViewModel viewModel)
        {
            BaseViewModel = viewModel;
            IsCommandDelete = false;
        }

        public readonly DependencyProperty IsCommandDeleteProperty = DependencyProperty.Register("IsCommandDelete",
                                                                                                typeof(bool),
                                                                                                typeof(AddDeleteCommand),
                                                                                                new PropertyMetadata(new bool()));
        
        public BaseCollectionViewModel BaseViewModel { get; set; }

        /// <summary>
        /// Указывает на то, используется ли команда для удаления элементов (true - для добавления)
        /// </summary>
        public bool IsCommandDelete
        {
            get => (bool)GetValue(IsCommandDeleteProperty);
            set => SetValue(IsCommandDeleteProperty, value);
        }

        public override bool CanExecute(object parameter)
        {
            return parameter is IFileSettings;
        }

        public override void Execute(object parameter)
        {
            if (IsCommandDelete)
            {
                BaseViewModel.AddElement((IFileSettings)parameter);
            }
            else
            {
                BaseViewModel.RemoveElement((IFileSettings)parameter);
            }
        }
    }
}