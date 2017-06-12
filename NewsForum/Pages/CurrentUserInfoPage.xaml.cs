using Model.UserTypes;
using NewsForum.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using ViewModelDataBase;
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
    public sealed partial class CurrentUserInfoPage : Page
    {
        User CloneCurrUser = new User();

        public CurrentUserInfoPage()
        {
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            CloneCurrUser.Convert(CurrentUser.User);
            this.InitializeComponent();
        }

        private async void ChangeUserButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var answer = await CurrentUser.Update(CloneCurrUser);
            if (answer)
            {
                ContentDialog noWifiDialog = new ContentDialog()
                {
                    Title = "Уведомление",
                    Content = "Ваши учетные данные успешно обновлены",
                    PrimaryButtonText = "Ok"
                };
                ContentDialogResult result = await noWifiDialog.ShowAsync();
            }
        }

        private async void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainPivot.SelectedIndex == 1)
            {
                await CurrentUser.GetSelfPublications();
                SelfFrame.Navigate(typeof(ContentPage), CurrentUser.User.ListPublications);
            }
            else
            {
                await CurrentUser.GetSelfComments();
                await CurrentUser.GetSelfRatings();
            }
        }

        private void ExitButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            CurrentUser.Exit();
            Frame.Navigate(typeof(LoginOrRegistrationPage));
        }
    }
}