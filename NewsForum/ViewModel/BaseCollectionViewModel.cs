using NewsForum.Model;
using NewsForum.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace NewsForum.ViewModel
{
    class BaseCollectionViewModel : DependencyObject
    {
        public AddDeleteCommand AddDeleteCommand { get; private set; }
       
        public ObservableCollection<IFileSettings> BaseFileCollection { get; private set; }

        public event Action<IFileSettings> RemoveItemCollectionEvent;

        public BaseCollectionViewModel()
        {
            AddDeleteCommand = new AddDeleteCommand(this);
            BaseFileCollection = new ObservableCollection<IFileSettings>();
        }

        public virtual void RemoveElement(IFileSettings element)
        {
            BaseFileCollection.Remove(element);
            RemoveItemCollectionEvent?.Invoke(element);
        }

        public virtual void AddRange(IEnumerable<IFileSettings> collection)
        {
            foreach (var item in collection)
            {
                BaseFileCollection.Add(item);
            }
        }

        public virtual void AddElement(IFileSettings element)
        {
            BaseFileCollection.Add(element);
        }
    }
}