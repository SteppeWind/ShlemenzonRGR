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
    class IsSelfPublicationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int publicationId = (int)value;

            if (CurrentUser.User.AccessLevel == UserAccessLevel.Goest)
                return Visibility.Collapsed;

            if (CurrentUser.User.AccessLevel >= UserAccessLevel.Admin)
                return Visibility.Visible;

            return CurrentUser.User.ListPublications.FirstOrDefault(p => p.PublicationId == publicationId) == null ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}