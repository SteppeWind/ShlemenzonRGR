using NewsForum.Model;
using NewsForum.View.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Windows.Input;
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
        
        public ThirdStepDistributionEditorPage()
        {
            this.InitializeComponent();
            EditPanelPublication.AddEditDescriptionBox(EditDescriptionBox);
        }


        async Task<ObservableCollection<ImageContainer>> getImages(IReadOnlyList<StorageFile> files)
        {
            ObservableCollection<ImageContainer> result = new ObservableCollection<ImageContainer>();
            foreach (var item in files)
            {
                using (var stream = await item.OpenAsync(FileAccessMode.Read))
                {
                    var bitmap = new BitmapImage();
                    await bitmap.SetSourceAsync(stream);
                    result.Add(new ImageContainer()
                    {
                        FullPath = item.Path,
                        Name = item.DisplayName,
                        BitMapImg = bitmap
                    });
                }
            }
            return result;
        }

        private async void OpenFilesButton_Tapped(object sender, TappedRoutedEventArgs e)
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

            var storageFIles = await fop.PickMultipleFilesAsync();
            if (storageFIles != null)
            {
                LoadPhotosProgress.IsActive = true;
                    (Resources["PhotoElementViewModel"] as ViewModel.PhotoElementsBaseViewModel).AddRange(await getImages(storageFIles));
                LoadPhotosProgress.IsActive = false;
            }
        }
    }
}