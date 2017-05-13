using NewsForum.Model;
using NewsForum.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class AddPhotosToPublicationUserControl : UserControl
    {
        public event Action<IEnumerable<StorageFile>> LoadFilesEvent;


        public AddPhotosToPublicationUserControl()
        {
            this.InitializeComponent();
        }

        async Task<ObservableCollection<ImageContainer>> getImages(IEnumerable<StorageFile> files)
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

        private async void SetFilesControl_LoadFilesEvent(IEnumerable<StorageFile> obj)
        {
            LoadPhotosProgress.IsActive = true;
            if (obj != null)
            {
                (Resources["CollectionViewModel"] as BaseCollectionViewModel).AddRange(await getImages(obj));
                LoadFilesEvent?.Invoke(obj);
            }
            LoadPhotosProgress.IsActive = false;
        }
    }
}