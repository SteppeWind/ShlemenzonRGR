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
        string ban = "";
        string unban = "";

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (bool)value ? ban : unban;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (string)value == "Разбанить" ? true : false; 
        }
    }
}