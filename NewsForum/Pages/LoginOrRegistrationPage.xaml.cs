using NewsForum.Model;
using NewsForum.Model.Exceptions;
using RequestServer.AnswerForRequest;
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
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace NewsForum.Pages
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class LoginOrRegistrationPage : Page
    {
        string login { get; set; }
        string password { get; set; }

        public LoginOrRegistrationPage()
        {
            this.InitializeComponent();
        }

        private async void AutorizeButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            bool res = false;
            try
            {
                res = await CurrentUser.Autorize(login, password);
                if (res)
                {
                    Frame.Navigate(typeof(CurrentUserInfoPage));
                }
                else
                {
                    ContentDialog noWifiDialog = new ContentDialog()
                    {
                        Title = "Уведомление",
                        Content = "Проверьте пару логин/пароль",
                        PrimaryButtonText = "Ok"
                    };
                    ContentDialogResult result = await noWifiDialog.ShowAsync();
                }
            }
            catch (IsBannedUserException ex)
            {
                ContentDialog noWifiDialog = new ContentDialog()
                {
                    Title = "Уведомление",
                    Content = ex.Message,
                    PrimaryButtonText = "Ok"
                };
                ContentDialogResult result = await noWifiDialog.ShowAsync();
            }
        }

        private void RegistrationButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(RegistrationPage));
        }
    }
}