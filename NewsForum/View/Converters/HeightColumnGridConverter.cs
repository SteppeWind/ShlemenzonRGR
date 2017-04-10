using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace NewsForum.View.Converters
{
    class HeightColumnGridConverter : IValueConverter
    {
        private Thickness expandHeight = new Thickness(0, 0, 0, 250);
        private Thickness collapseHeight = new Thickness(0);

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (bool)value ? expandHeight : collapseHeight;
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
