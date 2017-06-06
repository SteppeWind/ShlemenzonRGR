using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using ViewModelDataBase.VMPublicationTypes.VMNewsTypes;
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
    public sealed partial class LinkVideoViewUserControl : UserControl
    {

        public VMElementLinkVideo LinkVideoElement { get; set; }

        public string LinkVideo
        {
            get { return (string)GetValue(LinkVideoProperty); }
            set { SetValue(LinkVideoProperty, LinkVideoTextBox.Text = value); }
        }

        // Using a DependencyProperty as the backing store for LinkVideo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LinkVideoProperty =
            DependencyProperty.Register("LinkVideo", typeof(string), typeof(LinkVideoViewUserControl), new PropertyMetadata(0));



        public LinkVideoViewUserControl()
        {
            this.InitializeComponent();
            LinkVideoElement = new VMElementLinkVideo();
        }

        private void AddLinkButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            try
            {
                if (LinkVideoElement.SetResultCode(LinkVideoTextBox.Text))
                    LinkVideo = LinkVideoTextBox.Text;
                MyWebView.NavigateToString(LinkVideoElement.HTMLCode);
            }
            catch { }
        }

        private void DeleteLinkButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            RefreshWebView();
            LinkVideo = null;
        }

        public void RefreshWebView() => MyWebView.Refresh();
    }
}