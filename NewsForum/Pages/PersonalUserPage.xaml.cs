using NewsForum.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using ViewModelDataBase;
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
    public sealed partial class PersonalUserPage : Page
    {
        VMUser CurrUser { get; set; }

        public PersonalUserPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            CurrUser = e.Parameter as VMUser;
        }

        private async void ChangeUserButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var answer = await ServerRequest.SendRequest(new RequestServer.Request.MainRequest()
            {
                DataType = RequestServer.DataType.User,
                TypeRequest = RequestServer.Request.TypeRequest.Update,
                RecievedRequest = CurrUser,
                UserId = CurrUser.UserId
            });
            if ((bool)answer.SelfAnswer)
            {
                ContentDialog noWifiDialog = new ContentDialog()
                {
                    Title = "Уведомление",
                    Content = "Данные пользователя успешно обновлены",
                    PrimaryButtonText = "Ok"
                };
                ContentDialogResult result = await noWifiDialog.ShowAsync();
            }
        }
    }
}