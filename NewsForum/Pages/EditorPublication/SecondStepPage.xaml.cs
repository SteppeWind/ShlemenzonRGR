using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using ViewModelDataBase.VMPublicationTypes;
using ViewModelDataBase.VMPublicationTypes.VMNewsTypes;
using ViewModelDataBase.VMTypes;
using Windows.ApplicationModel.DataTransfer;
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

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace NewsForum.Pages.EditorPublication
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class SecondStepPage : Page
    {
        public VMPublication Publication { get; set; } = new VMPublication();


        public SecondStepPage()
        {
            this.InitializeComponent();
            //Publication = Resources["Publication"] as VMPublication;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Publication = e.Parameter as VMPublication;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            
        }

        private async void AddCoverPublicationUserControl_CompleteDropEvent(StorageFile obj)
        {
            using (Stream stream = await obj.OpenStreamForReadAsync())
            {
                BinaryReader br = new BinaryReader(stream);
                var bytes = br.ReadBytes((int)stream.Length);
                Publication.PosterImage = new VMFile()
                {
                    Bytes = bytes,
                    Type = obj.FileType
                };
            }
        }
    }
}