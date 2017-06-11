using Model.PublicationTypes;
using NewsForum.Model;
using NewsForum.Pages.ViewPublicationInfo;
using Newtonsoft.Json;
using RequestServer.AnswerForRequest;
using RequestServer.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using ViewModelDataBase;
using ViewModelDataBase.VMPublicationTypes;
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

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace NewsForum.Pages
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class ContentPage : Page
    {
        List<VMSmallPublication> ListPublications { get; set; }
        VMUser CurrUser = CurrentUser.User;


        public ContentPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is List<VMSmallPublication> res)
                ListPublications = res;
        }

        private async void PublicationsGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedItem = e.ClickedItem as VMSmallPublication;
            Answer answer = await ServerRequest.SendRequest(new MainRequest()
            {
                DataType = RequestServer.DataType.Publication,
                TypeRequest = TypeRequest.Read,
                RecievedRequest = selectedItem.PublicationId
            });
            switch (selectedItem.TypePublication)
            {
                case PublicationType.Game:
                    var gp = JsonConvert.DeserializeObject<VMGamePublication>(answer.SelfAnswer.ToString(), new JsonSerializerSettings()
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    });
                    Frame.Navigate(typeof(ViewInfoGamePublicationPage), gp);
                    break;

                case PublicationType.Film:
                    var fp = JsonConvert.DeserializeObject<VMFilmPublication>(answer.SelfAnswer.ToString(), new JsonSerializerSettings()
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    });
                    Frame.Navigate(typeof(ViewInfoFilmPublicationPage), fp);
                    break;

                case PublicationType.Music:
                    var mp = JsonConvert.DeserializeObject<VMMusicPublication>(answer.SelfAnswer.ToString(), new JsonSerializerSettings()
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    });
                   Frame.Navigate(typeof(ViewInfoMusicPublicationPage), mp);
                    break;

                case PublicationType.News:
                    var np = JsonConvert.DeserializeObject<VMNewsPublication>(answer.SelfAnswer.ToString(), new JsonSerializerSettings()
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    });
                    await FilesAction.CreateFilesPublication(np);
                    Frame.Navigate(typeof(ViewInfoNewsPublicationPage), np);
                    break;
            }

        }
    }
}