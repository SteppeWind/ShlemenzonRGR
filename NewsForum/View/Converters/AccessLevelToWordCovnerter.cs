using Model.UserTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace NewsForum.View.Converters
{
    class AccessLevelToWordCovnerter : IValueConverter
    {

        string[] words = { "", "", "Пользователь", "Модератор", "Админ" };
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return words[(int)(UserAccessLevel)value];
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            int index = 0;
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Contains(value.ToString()))
                    index = i;
            }
            return (UserAccessLevel)index;
        }
    }
}