using Model.UserTypes;
using NewsForum.Model;
using NewsForum.Pages.ViewPublicationInfo;
using Newtonsoft.Json;
using RequestServer.PublicationsRequest;
using RequestServer.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using ViewModelDataBase.VMPublicationTypes;
using ViewModelDataBase.VMPublicationTypes.VMNewsTypes;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace NewsForum.Pages.EditorPublication
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class LastStepPage : Page
    {
        VMPublication Publication;
        public LastStepPage()
        {
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.InitializeComponent();
            Publication = (e.Parameter as VMPublication);
            switch (Publication.TypePublication)
            {
                case global::Model.PublicationTypes.PublicationType.Game:
                    CurrentFrame.Navigate(typeof(ViewInfoGamePublicationPage), Publication);
                    break;
                case global::Model.PublicationTypes.PublicationType.Film:
                    CurrentFrame.Navigate(typeof(ViewInfoFilmPublicationPage), Publication);
                    break;
                case global::Model.PublicationTypes.PublicationType.Music:
                    CurrentFrame.Navigate(typeof(ViewInfoMusicPublicationPage), Publication);
                    break;
                case global::Model.PublicationTypes.PublicationType.News:
                    CurrentFrame.Navigate(typeof(ViewInfoNewsPublicationPage), Publication);
                    break;
                default:
                    break;
            }
        }


        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
        }

        private async Task SetDescritpion()
        {
            var des = FilesAction.Folder.Path + "\\Description.rtf";
            if (File.Exists(des))
            {
                var file = await FilesAction.Folder.GetFileAsync("Description.rtf");
                var result = await FilesAction.ConvertToIFileVM(file);
                Publication.Description = new ViewModelDataBase.VMTypes.VMFile()
                {
                    Bytes = result.Item2,
                    Type = result.Item1
                };
            }
        }

        private async void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Publication.UserId = 1;
            var tag = (sender as Button).Tag;

            switch (tag)
            {
               case "CreatePublication":
                    await SetDescritpion();
                    MainRequest mr = new MainRequest()
                    {
                        DataType = RequestServer.DataType.Publication,
                        TypeRequest = RequestServer.Request.TypeRequest.Create,
                        RecievedRequest = Publication
                    };
                    try
                    {
                        var answer = await ServerRequest.SendRequest(mr);
                        bool res = (bool)answer.SelfAnswer;
                        ContentDialog noWifiDialog = new ContentDialog()
                        {
                            Title = "Уведомление",
                            Content = "Запись успешно создана",
                            PrimaryButtonText = "Ok"
                        };
                        ContentDialogResult result = await noWifiDialog.ShowAsync();
                        
                    }
                    catch (Exception ex)
                    {
                    }
                    break;

                case "Cancel":

                    break;
                default:
                    break;
            }
        }
    }
}