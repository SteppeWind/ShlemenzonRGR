using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace NewsForum.View.MyUserControls
{
    interface IContentUserControl
    {
        //bool IsBelongEditorNewsPublication { get; set; }

        event TappedEventHandler DeleteStartingEvent;

        UserControl ContentUserControl { get; }

        void AddContent(UserControl UserControl);
    }
}