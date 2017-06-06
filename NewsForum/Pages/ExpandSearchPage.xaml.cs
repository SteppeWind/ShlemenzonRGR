using Model.PublicationTypes;
using NewsForum.Model;
using Newtonsoft.Json;
using RequestServer.PublicationsRequest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using ViewModelDataBase.VMPublicationTypes;
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
    public sealed partial class ExpandSearchPage : Page
    {
        ReadPublciationRequest Request = new ReadPublciationRequest();
        List<VMSmallPublication> listPublications { get; set; }
        public ExpandSearchPage()
        {
            this.InitializeComponent();
            GenresListView.ItemsSource = GenreTypes.GameGenres;
        }

        private void GenresCombox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GenresListView != null)
                switch (GenresCombox.SelectedIndex)
                {
                    case 0:
                        GenresListView.ItemsSource = GenreTypes.GameGenres;
                        break;

                    case 1:
                        GenresListView.ItemsSource = GenreTypes.FilmGenres;
                        break;

                    case 2:
                        GenresListView.ItemsSource = GenreTypes.MusicGenres;
                        break;
                    default:
                        break;
                }
        }

        private async void SearchButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Request.ListGenres = GenresListView.SelectedGenres.Select(g => g.Name).ToList();
            Request.LeftLimitTime = LeftData.Date.ToString("dd.MM.yyyy");
            Request.RightLimitTime = RightData.Date.ToString("dd.MM.yyyy");
            Request.PublicationType = (PublicationType)GenresCombox.SelectedIndex;
            var answer = await ServerRequest.SendRequest(new RequestServer.Request.MainRequest()
            {
                DataType = RequestServer.DataType.SmallPublication,
                TypeRequest = RequestServer.Request.TypeRequest.Read,
                RecievedRequest = Request,
                UserId = CurrentUser.User.UserId
            });
            listPublications = JsonConvert.DeserializeObject<List<VMSmallPublication>>(answer.SelfAnswer.ToString());
            await FilesAction.CreatePostersPublications(listPublications);
            MyFrame.Navigate(typeof(ContentPage), listPublications);
        }

        private void ExpandFrameButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(ContentPage), listPublications);
        }
    }
}