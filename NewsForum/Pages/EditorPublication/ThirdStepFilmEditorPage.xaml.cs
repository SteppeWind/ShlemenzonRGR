using Model.PublicationTypes;
using NewsForum.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModelDataBase.VMPublicationTypes;
using ViewModelDataBase.VMTypes;
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
    public sealed partial class ThirdStepDistributionEditorPage : Page
    {
        ObservableCollection<Actor> ListAutors = new ObservableCollection<Actor>();
        VMFilmPublication Publication { get; set; }
        public string[] Genres = GenreTypes.FilmGenres;

        public ThirdStepDistributionEditorPage()
        {
            this.InitializeComponent();
            ActorsListView.ItemsSource = ListAutors;
            EditPanelPublication.AddEditDescriptionBox(EditDescriptionBox);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Publication = e.Parameter as VMFilmPublication;
        }

        protected async override void OnNavigatedFrom(NavigationEventArgs e)
        {
            var file = await FilesAction.CreateLocalStorageFile("Description.rtf");
            await EditDescriptionBox.SaveDocumentStreamToFile(file);
            if (Publication != null)
            {
                Publication.ListGenres = GenresControl.SelectedGenres;
                Publication.ReleaseYear = RealeseDatePicker.GetCurrentDate.DateTime;
                Publication.ListFiles.Clear();
                Publication.ListActors = ListAutors.ToList();
                AddPhotosControl.ListPhotos.AsParallel().ForAll(async (p) => { await AddFileToFilesPublic(p); });
            }
        }

        private async Task AddFileToFilesPublic(StorageFile item)
        {
            var curr = await FilesAction.ConvertToIFileVM(item);
            Publication.ListFiles.Add(new VMFile()
            {
                Type = curr.Item1,
                Bytes = curr.Item2
            });
        }

        private void AddActorButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ListAutors.Add(new Actor());
        }

        private void DeleteActorsButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ListAutors.Remove(ActorsListView.SelectedItem as Actor);
        }
    }
}