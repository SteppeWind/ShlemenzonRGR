using NewsForum.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ViewModelDataBase.VMTypes;
using Windows.Storage;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace NewsForum.View.Converters
{
    class FileConverter : IValueConverter
    {
        private ImageContainer container { get; set; }

        private BitmapImage image { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var filePath = value as string;
            if (filePath != null)
            {
               return new BitmapImage(new Uri(filePath));
            }

            return new BitmapImage(new Uri(@"ms-appx:///Images/add_cover_publication.png"));
        }

        private async void Action(String filePath)
        {
            var newFile = await StorageFile.GetFileFromPathAsync(filePath);
            image.SetSource(await newFile.OpenReadAsync());
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

    }
}