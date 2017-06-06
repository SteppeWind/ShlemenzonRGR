using Model.PublicationTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace NewsForum.View.Converters
{
    class TypePublicationToWordConverter : IValueConverter
    {
        string[] words = { "Игры", "Фильмы", "Музыка", "Статья", "Все" };
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return words[(int)(PublicationType)value];
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            int index = 0;
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Contains(value.ToString()))
                    index = i;
            }
            return (PublicationType)index;
        }
    }
}