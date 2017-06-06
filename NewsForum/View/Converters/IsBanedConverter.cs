using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace NewsForum.View.Converters
{
    class IsBanedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (bool)value ? "Разбанить" : "Забанить";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (string)value == "Разбанить" ? true : false; 
        }
    }
}