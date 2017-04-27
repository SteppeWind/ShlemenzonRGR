using NewsForum.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
                    Name = item.DisplayName
                });
            }
            MediaPlayerControl.SetMusicFiles(collection);
        }
    }
}