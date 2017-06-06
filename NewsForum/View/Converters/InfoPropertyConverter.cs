using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace NewsForum.View.Converters
{
   public class InfoPropertyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                string val = value.ToString();
                if (value is DateTime date)
                {
                    val = date.ToString("dd.MM.yyyy");
                }

                if (value is bool @bool)
                {
                    val = @bool ? "Присутствует" : "Отсутствует";
                }
                return $"{parameter.ToString()}: {val}";
            }

            return $"{parameter.ToString()}: Пусто";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}