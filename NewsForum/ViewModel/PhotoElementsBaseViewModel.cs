using NewsForum.Model;
using NewsForum.View.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsForum.ViewModel
{
    class PhotoElementsBaseViewModel
    {
        public DeleteImageFromFotoElementsCommand DeleteElementCommand { get; set; }

        public ObservableCollection<ImageContainer> ListElements { get; set; }


        public PhotoElementsBaseViewModel()
        {
            ListElements = new ObservableCollection<ImageContainer>();
            DeleteElementCommand = new DeleteImageFromFotoElementsCommand(this);
        }

        public void RemoveElement(object element)
        {
            ListElements.Remove((ImageContainer)element);
        }

        public void AddRange(IEnumerable<ImageContainer> collection)
        { 
            foreach (var item in collection)
            {
                ListElements.Add(item);
            }
        }
    }
}
