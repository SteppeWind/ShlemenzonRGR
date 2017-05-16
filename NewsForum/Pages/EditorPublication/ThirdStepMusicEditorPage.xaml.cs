using Model.PublicationTypes;
using NewsForum.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace NewsForum.Pages.EditorPublication
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class ThirdStepMusicEditorPage : Page
    {
        public string[] Genres = GenreTypes.MusicGenres;
        VMMusicPublication Publication;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Publication = e.Parameter as VMMusicPublication;
        }

        protected async override void OnNavigatedFrom(NavigationEventArgs e)
        {
            var file = await FilesAction.CreateLocalStorageFile("Description.rtf");
            await EditDescriptionBox.SaveDocumentStreamToFile(file);            
            Publication.ListGenres = GenresControl.SelectedGenres;
            Publication.ReleaseYear = RealeseDatePicker.Date.DateTime;
            Publication.ListFiles.Clear();
            foreach (var item in AddPhotosControl.ListPhotos)
            {
                await AddFileToFilesPublic(item);
            }
            foreach (var item in MediaPlayerControl.ListMusic)
            {
                await AddFileToFilesPublic(item);
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

        public ThirdStepMusicEditorPage()
        {
            this.InitializeComponent();
            DecriptionPublicationControl.AddEditDescriptionBox(EditDescriptionBox);
        }

        private async void SelectFileControl_LoadFilesEvent(IEnumerable<Windows.Storage.StorageFile> obj)
        {
            ObservableCollection<SoundFileContainer> collection = new ObservableCollection<SoundFileContainer>();
            foreach (var item in obj)
            {
                collection.Add(new SoundFileContainer()
                {
                    AccessStream = await item.OpenReadAsync(),
                    FullPath = item.Path,
                    Name = item.DisplayName,
                    File = item
                });
                MediaPlayerControl.AddMusicFile(item);
            }
            MediaPlayerControl.SetMusicFiles(collection);
        }
    }
}