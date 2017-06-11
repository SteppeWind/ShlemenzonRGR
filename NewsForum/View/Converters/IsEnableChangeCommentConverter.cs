using Cimbalino.Toolkit.Converters;
using Model.UserTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelDataBase.VMPublicationTypes;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using System.Globalization;

namespace NewsForum.View.Converters
{
    class IsEnableChangeCommentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var currUser = (value as object[])[0] as User;
            int currCommentId = (int)(value as object[])[1];
            var comment = CurrentUser.User.ListComments.FirstOrDefault(c => c.CommentId == currCommentId);

            if (comment != null)
                return Visibility.Visible;

            if (currUser.AccessLevel <= UserAccessLevel.Admin && CurrentUser.User.AccessLevel >= UserAccessLevel.Admin)
                return Visibility.Visible;

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}