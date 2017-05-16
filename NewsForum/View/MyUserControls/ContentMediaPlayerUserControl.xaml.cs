using NewsForum.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using NewsForum.ViewModel;
using ViewModelDataBase.VMTypes;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace NewsForum.View.MyUserControls
{
    public sealed partial class ContentMediaPlayerUserControl : UserControl
    {
        public List<StorageFile> ListMusic { get; private set; } = new List<StorageFile>();

        MediaElement CurrentMedia { get; set; }

        public static readonly DependencyProperty IsEditMusicCollectionProperty = DependencyProperty.Register("IsEditMusicCollection",
                                                                                  typeof(bool),
                                                                                  typeof(ContentMediaPlayerUserControl),
                                                                                  new PropertyMetadata(false));
        public bool IsEditMusicCollection
        {
            get => (bool)GetValue(IsEditMusicCollectionProperty);
            set => SetValue(IsEditMusicCollectionProperty, value);
        }
        
        public ContentMediaPlayerUserControl()
        {
            this.InitializeComponent();
            CurrentMedia = new MediaElement();
        }
        

        public void SetSource(IRandomAccessStream stream)
        {
            CurrentMedia.SetSource(stream, "");
        }

        internal void SetMusicFiles(ObservableCollection<SoundFileContainer> collection)
        {
            (Resources["PlayerViewModel"] as BaseCollectionViewModel).AddRange(collection);
        }

        private void PauseOrPlayButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (CurrentMedia.CurrentState == MediaElementState.Playing)
            {
                PauseOrPlayButton.Icon = new SymbolIcon(Symbol.Pause);
            }
            else if (CurrentMedia.CurrentState == MediaElementState.Paused)
            {
                PauseOrPlayButton.Icon = new SymbolIcon(Symbol.Play);
            }
        }

        private void MediaPlayerViewModel_RemoveItemCollectionEvent(IFileSettings obj)
        {
            if (obj != null)
                ListMusic.Remove(obj.File);
        }

        public void AddMusicFile(StorageFile file)
        {
            if (file != null)
                ListMusic.Add(file);
        }
    }
}