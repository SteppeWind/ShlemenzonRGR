using NewsForum.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using ViewModelDataBase.VMPublicationTypes;
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

namespace NewsForum.Pages.ViewPublicationInfo
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class ViewInfoMusicPublicationPage : Page
    {
        VMMusicPublication Publication { get; set; }

        public ViewInfoMusicPublicationPage()
        {
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Publication = e.Parameter as VMMusicPublication;
             a();
        }


        async void a()
        {
            await Task.Delay(100);
            await FilesAction.CreateFilesPublication(Publication);
            var file = await FilesAction.Folder.GetFileAsync("Description.rtf");
            this.InitializeComponent();
            using (var stream = await file.OpenAsync(FileAccessMode.Read))
            {
                DescriptionRichEditBox.Document.LoadFromStream(Windows.UI.Text.TextSetOptions.FormatRtf, stream);
            }
            ObservableCollection<SoundFileContainer> songs = new ObservableCollection<SoundFileContainer>();
            foreach (var item in Publication.ListSongs)
            {
                file = await StorageFile.GetFileFromPathAsync(item.FullPath);
                songs.Add(new SoundFileContainer()
                {
                    Name = item.Name,
                    File = file,
                    AccessStream = await file.OpenReadAsync(),
                    FullPath = file.Path
                });
            }
            MediaPlayer.SetMusicFiles(songs);
        }
    }
}