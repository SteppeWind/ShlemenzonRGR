using Model.UserTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NewsForum.View.Selectors
{
    class AccessUserSelector : DataTemplateSelector
    {
        public DataTemplate AdminTemplate { get; set; }

        public DataTemplate GodTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (CurrentUser.User.AccessLevel == UserAccessLevel.God)
                return GodTemplate;

            return AdminTemplate;
        }
    }
}