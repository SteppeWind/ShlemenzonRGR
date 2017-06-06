using Model.PublicationTypes;
using Model.UserTypes;
using NewsForum.Model;
using NewsForum.Pages;
using NewsForum.Pages.EditorPublication;
using NewsForum.Pages.ViewPublicationInfo;
using NewsForum.View.Converters;
using Newtonsoft.Json;
using RequestServer.PublicationsRequest;
using RequestServer.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using ViewModelDataBase;
using ViewModelDataBase.VMPublicationTypes;
using Windows.Data.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
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
            Start();
            //go();
        }


        async void Start()
        {
            var answer = await ServerRequest.SendRequest(new MainRequest()
            {
                DataType = RequestServer.DataType.SmallPublication,
                TypeRequest = TypeRequest.Read,
                RecievedRequest = new ReadPublciationRequest()
                {
                    PublicationType = PublicationType.Any,
                },
                UserId = CurrentUser.User.UserId

            });


            var listPublications = JsonConvert.DeserializeObject<List<VMSmallPublication>>(answer.SelfAnswer.ToString());
            await FilesAction.CreatePostersPublications(listPublications);
            MyFrame.Navigate(typeof(ContentPage), listPublications);
        }

        async void go()
        {
           //var res = await CurrentUser.Autorize("89137176239", "2018");
           // var re2 = await CurrentUser.User.SetSelfPublications();
           // ReadPublciationRequest rpr = new ReadPublciationRequest()
            //{
            //    PublicationType = PublicationType.Any
            //};
            //MainRequest mr = new MainRequest()
            //{
            //    RecievedRequest = rpr,
            //    DataType = RequestServer.DataType.Publication,
            //    TypeRequest = TypeRequest.Read,
            //    UserId = 1
            //};

            //ServerRequest.GetAnswerForLastRequest += async (answer) =>
            //{
            //    switch (answer.TypeAnswer)
            //    {
            //        case RequestServer.DataType.Bool:
            //            break;
            //        case RequestServer.DataType.Publication:
            //            break;
            //        case RequestServer.DataType.User:
            //            break;
            //        case RequestServer.DataType.Actor:
            //            break;
            //        case RequestServer.DataType.SmallPublication:
            //            List<VMSmallPublication> listPublications = JsonConvert.DeserializeObject<List<VMSmallPublication>>(answer.SelfAnswer.ToString());
            //            await FilesAction.CreatePostersPublications(listPublications);
            //            MyFrame.Navigate(typeof(ContentPage), listPublications);
            //            break;
            //        default:
            //            break;
            //    }
            //};
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MenuSplitView.IsPaneOpen = !MenuSplitView.IsPaneOpen;
        }

        private async void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var key = (e.ClickedItem as StackPanel);
            if (key != null)
                switch (key.Tag)
                {
                    case "Profile":
                        if (CurrentUser.User.AccessLevel == UserAccessLevel.Goest)
                            MyFrame.Navigate(typeof(LoginOrRegistrationPage));
                        else
                            MyFrame.Navigate(typeof(CurrentUserInfoPage));
                        break;
                    case "ExpandSearch":
                        MyFrame.Navigate(typeof(ExpandSearchPage));
                        break;
                    case "Home":
                        Start();
                        break;
                    case "Publication":
                        MyFrame.Navigate(typeof(NavigationPage));
                        break;

                    case "NonPublished":
                        break;
                    case "Users":
                        var answer = await ServerRequest.SendRequest(new MainRequest()
                        {
                            DataType = RequestServer.DataType.User,
                            TypeRequest = TypeRequest.Read,
                            UserId = CurrentUser.User.UserId
                        });
                        MyFrame.Navigate(typeof(InfoUsersPage), JsonConvert.DeserializeObject<List<VMUser>>(answer.SelfAnswer.ToString()));
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