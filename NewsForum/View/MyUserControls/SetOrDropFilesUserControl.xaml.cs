using NewsForum.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
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
using Windows.Storage;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace NewsForum.View.MyUserControls
{
    public sealed partial class SetOrDropFilesUserControl : UserControl
    {
        public enum TypeFile
        {
            Image,
            Music
        }

        public static readonly DependencyProperty TypeFileProperty = DependencyProperty.Register("FileType",
                                                                                        typeof(TypeFile),
                                                                                        typeof(SetOrDropFilesUserControl),
                                                                                        new PropertyMetadata(TypeFile.Image));
        private string[] currentFilters;
        private string[] currentContentTypes;

        public TypeFile FileType
        {
            get => (TypeFile)GetValue(TypeFileProperty);
            set
            {
                switch (value)
                {
                    case TypeFile.Image:
                        currentFilters = FileExplorer.FiltersImage;
                        currentContentTypes = FileExplorer.ContentImageTypes;
                        TypeMessageTextBlock.Text = "Перетащите сюда фото-материалы";
                        break;
                    case TypeFile.Music:
                        currentFilters = FileExplorer.FiltersMusic;
                        currentContentTypes = FileExplorer.ContentMusicTypes;
                        TypeMessageTextBlock.Text = "Перетащите сюда аудио файлы";
                        break;
                }
                SetValue(TypeFileProperty, value);
            }
        }

        public event Action<IEnumerable<StorageFile>> LoadFilesEvent;


        public SetOrDropFilesUserControl()
        {
            this.InitializeComponent();
        }

        private void MainGrid_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Copy;
            e.DragUIOverride.Caption = "Перетаскивай блять быстрее";
            e.DragUIOverride.IsCaptionVisible = true;
            e.DragUIOverride.IsContentVisible = true;
            e.DragUIOverride.IsGlyphVisible = true;
        }

        private void MainGrid_DragStarting(UIElement sender, DragStartingEventArgs args)
        {
            args.AllowedOperations = DataPackageOperation.Link;
        }

        private async void OpenFilesButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FileExplorer fileDialog = new FileExplorer(PickerLocationId.PicturesLibrary);
            fileDialog.SetFilterType(currentFilters);
            fileDialog.LoadEnded += fileDialog_LoadEnded;
            await fileDialog.PickMultipleFilesAsync();
        }

        private void fileDialog_LoadEnded(IEnumerable<StorageFile> obj) => LoadFilesEvent?.Invoke(obj);

        private async void MainGrid_Drop(object sender, DragEventArgs e)
        {
            if (e.DataView.Contains(StandardDataFormats.StorageItems))
            {
                var storageItems = await e.DataView.GetStorageItemsAsync();
                if (storageItems.Any())
                {
                    fileDialog_LoadEnded(from item in storageItems
                                         let contentType = (item as StorageFile).ContentType
                                         where currentContentTypes.Contains(contentType)
                                         select item as StorageFile);
                }
            }
        }
    }
}
