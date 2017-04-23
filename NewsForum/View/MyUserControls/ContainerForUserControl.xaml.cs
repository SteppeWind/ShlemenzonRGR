using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace NewsForum.View.MyUserControls
{
    public sealed partial class ContainerForUserControl : UserControl, IContentUserControl
    {
        public ContainerForUserControl()
        {
            this.InitializeComponent();
            FirstAppereanceStoryboard.Begin();
        }
        
        public UserControl ContentUserControl { get; private set; }

        public event TappedEventHandler DeleteStartingEvent;

        public void AddContent(UserControl userControl)
        {
            ContentUserControl = userControl;
            Grid.SetColumn(userControl, 1);
            MainGrid.Children.Add(userControl);
        }

        private void DeleteMainButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            DeleteStoryBoard.Begin();
        }

        private void DeleteStoryBoard_Completed(object sender, object e)
        {
            if (ContentUserControl.GetType() == typeof(LinkVideoViewUserControl))
            {
                (ContentUserControl as LinkVideoViewUserControl).RefreshWebView();
            }
            DeleteStartingEvent.Invoke(this, null);
        }
    }
}