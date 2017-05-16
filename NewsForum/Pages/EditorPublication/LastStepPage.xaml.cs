using Model.UserTypes;
using NewsForum.Model;
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
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var currPublic = (e.Parameter as VMPublication);
            switch (currPublic.TypePublication)
            {
                case global::Model.PublicationTypes.PublicationType.Game:
                    Publication = currPublic as VMGamePublication;
                    break;
                case global::Model.PublicationTypes.PublicationType.Film:
                    break;
                case global::Model.PublicationTypes.PublicationType.Music:
                    break;
                case global::Model.PublicationTypes.PublicationType.News:
                    break;
                default:
                    break;
            }

        }


        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
        }

        private async void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Publication.User = new ViewModelDataBase.VMUser() { AccessLevel = UserAccessLevel.God, UserId = 1 };
            MainRequest mr = new MainRequest()
            {
                DataType = RequestServer.DataType.Publication,
                TypeRequest = RequestServer.Request.TypeRequest.Create,
                RecievedRequest = Publication
            };
            try
            {
                await ServerRequest.SendRequest(mr);
                ServerRequest.GetAnswerForLastRequest += (res) =>
                  {
                      switch (res.TypeAnswer)
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
                  };
            }
            catch (Exception)
            {
            }
        }

    }
}