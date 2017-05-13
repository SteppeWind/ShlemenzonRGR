using Model.PublicationTypes;
using NewsForum.Model;
using NewsForum.Pages;
using NewsForum.Pages.EditorPublication;
using Newtonsoft.Json;
using RequestServer.PublicationsRequest;
using RequestServer.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Json;
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
            go();
         }

        async void go()
        {
            ReadPublciationRequest rpr = new ReadPublciationRequest()
            {
                LeftLimitTime = "1",
                ListGenres = new List<string>() { "азаз", "кек", "лол" },
                PublicationType = PublicationType.Game,
                RightLimitTime = "2"
            };
            MainRequest mr = new MainRequest()
            {
                RecievedRequest = rpr,
                DataType = RequestServer.DataType.Publication,
                TypeRequest = TypeRequest.Read,
                UserId = 1
            };
            var answer = await ServerRequest.SendRequest(mr);
            switch (answer.TypeAnswer)
            {
                case RequestServer.AnswerForRequest.TypeAnswer.Bool:
                    break;
                case RequestServer.AnswerForRequest.TypeAnswer.Publications:
                    break;
                case RequestServer.AnswerForRequest.TypeAnswer.Users:
                    break;
                default:
                    break;
            }
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
                        MyFrame.Navigate(typeof(NavigationPage));
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