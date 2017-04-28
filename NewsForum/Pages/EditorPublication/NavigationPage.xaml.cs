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

        public NavigationPage()
        {
            this.InitializeComponent();            
            NavigationEditFrame.SourcePageType = typeof(SecondStepPage);
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
                    NavigationEditFrame.Navigate(typeof(ThirdStepNewsEditorPage));
                    break;

                case 3:
                    SecondToThirdStepStoryBoard.Begin();
                    ForwardPageButton.IsEnabled = false;
                    NavigationEditFrame.Navigate(typeof(LastStepPage));
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