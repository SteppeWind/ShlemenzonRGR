using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
        public StorageFile ImageFile { get; private set; }
        public event Action<StorageFile> CompleteDropEvent;

        public AddCoverPublicationUserControl()
        {
            this.InitializeComponent();
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
                if (contentType == "image/png" || contentType == "image/jpeg")
                {
                    await bitMapImage.SetSourceAsync(await storageFile.OpenReadAsync());
                    CoverPublicationImage.Source = bitMapImage;
                    DeleteImageButton.IsEnabled = true;
                    CoverPublicationImage.Opacity = 1.0;
                    ImageFile = storageFile;
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
            FileOpenPicker fop = new FileOpenPicker()
            {
                CommitButtonText = "Открыть",
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.PicturesLibrary,
            };
            fop.FileTypeFilter.Add(".jpeg");
            fop.FileTypeFilter.Add(".jpg");
            fop.FileTypeFilter.Add(".png");
            var storageFile = await fop.PickSingleFileAsync();
            SetSourceCoverImageAsync(storageFile);
        }
    }
}