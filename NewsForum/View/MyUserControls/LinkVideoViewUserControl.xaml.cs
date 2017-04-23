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
    public sealed partial class LinkVideoViewUserControl : UserControl
    {
        string link = @"https://www.youtube.com/embed/";
        public String HTMLCode { get; private set; }
        public String LinkForVideo { get; private set; }

        public LinkVideoViewUserControl()
        {
            this.InitializeComponent();
        }

        private void AddLinkButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            try
            {
                LinkForVideo = link + LinkVideoTextBox.Text.Split('=')[1];
                string frame = $@"<center><iframe width=""560"" height=""315"" src=""{LinkForVideo}"" frameborder=""0"" allowfullscreen></iframe></center>";
                MyWebView.NavigateToString(frame);
                HTMLCode = frame;
            }
            catch { }
        }

        private void DeleteLinkButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            RefreshWebView();
        }

        public void RefreshWebView() => MyWebView.Refresh();
    }
}