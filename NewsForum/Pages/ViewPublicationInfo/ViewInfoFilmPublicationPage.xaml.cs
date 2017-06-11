using Model.PublicationTypes;
using Model.UserTypes;
using NewsForum.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using ViewModelDataBase;
using ViewModelDataBase.VMPublicationTypes;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace NewsForum.Pages.ViewPublicationInfo
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class ViewInfoFilmPublicationPage : Page
    {
        VMFilmPublication Publication { get; set; }
        
        
        public ViewInfoFilmPublicationPage()
        {
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Publication = e.Parameter as VMFilmPublication;
            a();
        }

        async void a()
        {
            await Task.Delay(100);
            await FilesAction.CreateFilesPublication(Publication);
            var file = await FilesAction.Folder.GetFileAsync("Description.rtf");
            this.InitializeComponent();

            if (Publication.LinkVideo.LinkYoutubeVideo != null)
                TrailerWebView.NavigateToString(Publication.LinkVideo.HTMLCode);

            await DescriptionRichEditBox.LoadDocumentsStreamToBox(file);
        }

        private void CommentsControl_InfoUserEvent(User obj)
        {
            VMUser user = new VMUser();
            user.Convert(obj);
            Frame.Navigate(typeof(PersonalUserPage), user);
        }
    }
}