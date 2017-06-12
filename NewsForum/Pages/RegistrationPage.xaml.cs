using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using NewsForum.Model;
using System.Threading.Tasks;
using Model.UserTypes;
using ViewModelDataBase;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace NewsForum.Pages
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class RegistrationPage : Page
    {
        VMUser CurrUser { get; set; }

        public RegistrationPage()
        {
            CurrUser = new VMUser();
            this.InitializeComponent();
        }

        private async void CreateUserButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (CurrUser.IsCorrectUserForAutorize)
            {
                var res = await CurrentUser.Registration(CurrUser);
                if (res)
                {
                    CurrentUser.User.Convert(CurrUser);
                    Frame.Navigate(typeof(CurrentUserInfoPage));
                }
            }
            else
            {
            }
        }

        private void CancelButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(ContentPage));
        }
    }
}