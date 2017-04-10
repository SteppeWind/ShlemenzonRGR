using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace NewsForum.View.Converters
{
    class ExpandSearchCollapseGenresConverter : IValueConverter
    {
        private string collapseCode = "";
        private string expandCode = "";

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (bool)value? collapseCode : expandCode;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
