using Model.UserTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace NewsForum.View.Converters
{
    class IsEnabledFromUserBannedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            UserAccessLevel levelCurrUser = CurrentUser.User.AccessLevel;
            UserAccessLevel levelUser = (UserAccessLevel)value;

            if (levelCurrUser == UserAccessLevel.God)
                return true;
            
            if (levelCurrUser > levelUser)
                return true;

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}