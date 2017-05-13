using Model.PublicationTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace NewsForum.View.Converters
{
    class IntToTypePublicationConverter : IValueConverter
    {
        //Game
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (int)(PublicationType)value;
        }

        //2
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (PublicationType)(int)value;
        }
    }
}