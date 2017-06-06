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
        VMUser CurrUser = CurrentUser.User;
        VMUser CloneCurrUser = new VMUser();


        List<VMSmallPublication> SelfListPublications { get; set; }

        public CurrentUserInfoPage()
        {
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            CloneCurrUser.Convert(CurrUser);
            if (CurrUser.ListPublications == null)
            {
                SelfListPublications = await CurrentUser.GetSelfPublications();
            }
            this.InitializeComponent();
        }

        private async void ChangeUserButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var answer = await ServerRequest.SendRequest(new RequestServer.Request.MainRequest()
            {
                DataType = RequestServer.DataType.User,
                TypeRequest = RequestServer.Request.TypeRequest.Update,
                RecievedRequest = CloneCurrUser,
                UserId = CurrUser.UserId
            });
            if ((bool)answer.SelfAnswer)
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

        private async void SelfPublicationsPivotItem_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}