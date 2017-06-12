using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace NewsForum.View.Converters
{
    class IsPublishedMessageConverter : IValueConverter
    {
        string isModerated = "";
        string isPublished = "";

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (bool)value ? isPublished : isModerated;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}