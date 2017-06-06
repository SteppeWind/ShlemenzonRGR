using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace NewsForum.View.Converters
{
    class TrueOrFalseConverter : IValueConverter
    {
        private string isTrue { get; } = "";
        private string isFalse { get; } = "";


        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return string.Format($"{parameter}: ", (bool)value ? "Присутствует" : "Отсутствует");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}