using Model.UserTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace NewsForum.View.Converters
{
    class UserAccessVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            UserAccessLevel level = UserAccessLevel.User;
            return CurrentUser.User.AccessLevel >= level ? true : false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return CurrentUser.User.AccessLevel;
        }
    }
}