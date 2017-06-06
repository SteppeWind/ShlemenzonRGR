using NewsForum.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace NewsForum.View.MyUserControls
{
    public sealed partial class AddCoverPublicationUserControl : UserControl
    {
        public event Action<StorageFile> CompleteDropEvent;
        public event Action DeletePosterEvent;
        private FileExplorer FileDialog;
        public VMFile ImageFile { get; private set; }


        public AddCoverPublicationUserControl()
        {
            this.InitializeComponent();
            FileDialog = new FileExplorer(PickerLocationId.PicturesLibrary);
            FileDialog.SetFilterType(FileExplorer.FiltersImage);
        }

        private void CoverPublicationImage_OnDragStarting(UIElement sender, DragStartingEventArgs args)
        {
            args.AllowedOperations = DataPackageOperation.Link;
        }

        private async void CoverPublicationImage_OnDrop(object sender, DragEventArgs e)
        {
            if (e.DataView.Contains(StandardDataFormats.StorageItems))
            {
                var storageItems = await e.DataView.GetStorageItemsAsync();
                if (storageItems.Any())
                {
                    var storageFile = storageItems[0] as StorageFile;
                    SetSourceCoverImageAsync(storageFile);
                }
            }
        }

        private async void SetSourceCoverImageAsync(StorageFile storageFile)
        {
            if (storageFile != null)
            {
                var bitMapImage = new BitmapImage();
                var contentType = storageFile.ContentType;
                if (FileExplorer.ContentImageTypes.Contains(contentType))
                {
                    await bitMapImage.SetSourceAsync(await storageFile.OpenReadAsync());
                    CoverPublicationImage.Source = bitMapImage;
                    DeleteImageButton.IsEnabled = true;
                    CoverPublicationImage.Opacity = 1.0;
                    using (Stream stream = await storageFile.OpenStreamForReadAsync())
                    {
                        BinaryReader br = new BinaryReader(stream);
                        var bytes = br.ReadBytes((int)stream.Length);
                        ImageFile = new VMFile()
                        {
                            Name = storageFile.DisplayName,
                            Bytes = bytes,
                            Type = storageFile.FileType
                        };
                    }
                    CompleteDropEvent?.Invoke(storageFile);
                }
            }
        }

        private void CoverPublicationImage_OnDragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Copy;
            e.DragUIOverride.Caption = "Перенесите уже картинку";
            e.DragUIOverride.IsCaptionVisible = true;
            e.DragUIOverride.IsContentVisible = true;
            e.DragUIOverride.IsGlyphVisible = true;
        }

        private async void OpenPickerButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var storageFile = await FileDialog.PickSingleFileAsync();
            SetSourceCoverImageAsync(storageFile);
        }

        private void DeleteImageButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ImageFile = null;
            DeletePosterEvent?.Invoke();
        }
    }
}