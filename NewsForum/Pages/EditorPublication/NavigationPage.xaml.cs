using NewsForum.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using ViewModelDataBase.VMPublicationTypes;
using ViewModelDataBase.VMPublicationTypes.VMNewsTypes;
using ViewModelDataBase.VMTypes;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
        VMGamePublication GamePublication;
        VMNewsPublication NewsPublication;
        VMFilmPublication FilmPublication;
        VMMusicPublication MusicPublication;

        public NavigationPage()
        {
            this.InitializeComponent();
            NavigationEditFrame.Navigate(typeof(SecondStepPage), Publication);
            NavigationEditFrame.Navigated += Frame_Navigated;
        }

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {
        }

        private void ForwardPageButton_Tapped(object sender, TappedRoutedEventArgs e)
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
                            NavigationEditFrame.Navigate(typeof(ThirdStepMusicEditorPage));
                            break;
                        case global::Model.PublicationTypes.PublicationType.News:
                            NavigationEditFrame.Navigate(typeof(ThirdStepNewsEditorPage));
                            break;
                        case global::Model.PublicationTypes.PublicationType.Film:
                            NavigationEditFrame.Navigate(typeof(ThirdStepDistributionEditorPage));
                            break;
                        default:
                            break;
                    }
                    break;

                case 3:
                    SecondToThirdStepStoryBoard.Begin();
                    //Вот тут бля
                    ForwardPageButton.IsEnabled = false;
                    NavigationEditFrame.Navigate(typeof(LastStepPage), Publication);
                    switch (Publication.TypePublication)
                    {
                        case global::Model.PublicationTypes.PublicationType.Game:
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
                    break;
           
                default:
                    break;
            }
        }

        private void BackPageButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            --CurrentStep;
            NavigationEditFrame.GoBack();
            SetEnNavigationButtons();
            switch (CurrentStep)
            {
                case 1:
                    SecondToFirstStepStoryBoard.Begin();
                    BackPageButton.IsEnabled = false;
                    break;

                case 2:
                    ThirdToSecondStepStoryBoard.Begin();
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