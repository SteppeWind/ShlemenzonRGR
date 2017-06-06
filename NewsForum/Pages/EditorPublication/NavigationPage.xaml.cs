using NewsForum.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using ViewModelDataBase.VMPublicationTypes;
using ViewModelDataBase.VMPublicationTypes.VMNewsTypes;
using ViewModelDataBase.VMTypes;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace NewsForum.Pages.EditorPublication
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class NavigationPage : Page
    {
        public int CurrentStep = 1;
        public VMPublication Publication { get; set; } = new VMPublication();

        public NavigationPage()
        {
            this.InitializeComponent();
            NavigationEditFrame.Navigate(typeof(SecondStepPage), Publication);
            NavigationEditFrame.Navigated += Frame_Navigated;
        }

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {
        }

        private async void ForwardPageButton_TappedAsync(object sender, TappedRoutedEventArgs e)
        {
            ++CurrentStep;
            SetEnNavigationButtons();
            switch (CurrentStep)
            {
                case 1:
                    break;

                case 2:
                    FirstToSecondStepStoryBoard.Begin();
                    switch (Publication.TypePublication)
                    {
                        case global::Model.PublicationTypes.PublicationType.Game:
                            Publication = new VMGamePublication()
                            {
                                PosterImage = Publication.PosterImage,
                                Title = Publication.Title
                            };
                            NavigationEditFrame.Navigate(typeof(ThirdStepGamePage), Publication);
                            break;
                        case global::Model.PublicationTypes.PublicationType.Music:
                            Publication = new VMMusicPublication()
                            {
                                PosterImage = Publication.PosterImage,
                                Title = Publication.Title
                            };
                            NavigationEditFrame.Navigate(typeof(ThirdStepMusicEditorPage), Publication);
                            break;
                        case global::Model.PublicationTypes.PublicationType.News:
                            Publication = new VMNewsPublication()
                            {
                                PosterImage = Publication.PosterImage,
                                Title = Publication.Title
                            };
                            NavigationEditFrame.Navigate(typeof(ThirdStepNewsEditorPage), Publication);
                            break;
                        case global::Model.PublicationTypes.PublicationType.Film:
                            Publication = new VMFilmPublication()
                            {
                                PosterImage = Publication.PosterImage,
                                Title = Publication.Title
                            };
                            NavigationEditFrame.Navigate(typeof(ThirdStepDistributionEditorPage), Publication);
                            break;
                        default:
                            break;
                    }
                    break;

                case 3:
                    SecondToThirdStepStoryBoard.Begin();
                    //Вот тут бля
                    foreach (var item in await FilesAction.Folder.GetItemsAsync())
                    {
                        await item.DeleteAsync();
                    }
                    if (Publication.PosterImage != null)
                    {
                        var file = await FilesAction.CreateLocalStorageFile(Guid.NewGuid().ToString() + Publication.PosterImage.Type, Publication.PosterImage.Bytes);
                        Publication.PosterImage.FullPath = file.Path;
                    }
                    ForwardPageButton.IsEnabled = false;
                    NavigationEditFrame.Navigate(typeof(LastStepPage), Publication);
                    break;

                default:
                    break;
            }
        }

        private void BackPageButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            --CurrentStep;
            SetEnNavigationButtons();
            switch (CurrentStep)
            {
                case 1:
                    SecondToFirstStepStoryBoard.Begin();
                    NavigationEditFrame.Navigate(typeof(SecondStepPage), Publication);
                    BackPageButton.IsEnabled = false;
                    break;

                case 2:
                    ThirdToSecondStepStoryBoard.Begin();
                    NavigationEditFrame.GoBack();
                    break;

                case 3:
                    break;

                default:
                    break;
            }
        }

        private void SetEnNavigationButtons()
        {
            ForwardPageButton.IsEnabled = true;
            BackPageButton.IsEnabled = true;
        }
    }
}