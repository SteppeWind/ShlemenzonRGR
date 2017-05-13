using Model.PublicationTypes;
using NewsForum.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using ViewModelDataBase.VMPublicationTypes;
using ViewModelDataBase.VMTypes;
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

namespace NewsForum.Pages.EditorPublication
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class ThirdStepGamePage : Page
    {
        public string[] Genres = GenreTypes.GameGenres;
        public VMGamePublication Publication { get; set; } = new VMGamePublication();


        public ThirdStepGamePage()
        {
            this.InitializeComponent();
            DecriptionPublicationControl.AddEditDescriptionBox(EditDescriptionBox);
        }

        protected async override void OnNavigatedFrom(NavigationEventArgs e)
        {
            var doc = EditDescriptionBox.Document;
            var file = await FilesAction.CreateLocalStorageFile("Description.rtf");
            using (var stream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                doc.SaveToStream(Windows.UI.Text.TextGetOptions.FormatRtf, stream);
            }
            var result = await FilesAction.ConvertToIFileVM(file);
            Publication.Description = new ViewModelDataBase.VMTypes.VMDescription()
            {
                Bytes = result.Item2,
                Type = result.Item1
            };
            Publication.ListGenres = GenresListView.SelectedGenres;
            Publication.CreateDate = RealeseDatePicker.Date.DateTime;
        } 

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Publication = e.Parameter as VMGamePublication;
        }

        private async void AddPhotosToPublicationUserControl_LoadFilesEvent(IEnumerable<StorageFile> obj)
        {
            foreach (var file in obj)
            {
                var curr = await FilesAction.ConvertToIFileVM(file);
                Publication.ListImages.Add(new VMImage()
                {
                    Type = curr.Item1,
                    Bytes = curr.Item2
                });
            }
        }
    }
}