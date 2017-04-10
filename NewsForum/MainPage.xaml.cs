using NewsForum.Pages;
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

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace NewsForum
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MenuSplitView.IsPaneOpen = !MenuSplitView.IsPaneOpen;
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var key = (e.ClickedItem as StackPanel);
            if (key != null)
                switch (key.Tag)
                {
                    case "Profile":
                        MyFrame.Navigate(typeof(LoginOrRegistrationPage));
                        break;
                    case "ExpandSearch":
                        MyFrame.Navigate(typeof(ExpandSearchPage));
                        break;
                    case "Home":
                        break;
                    case "Publication":
                        //MyFrame.Navigate(typeof(EditorPublicationsPage));
                        break;
                    default:
                        break;
                }
        }

        private void HamburgerButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MenuSplitView.IsPaneOpen = !MenuSplitView.IsPaneOpen;
        }
    }
}