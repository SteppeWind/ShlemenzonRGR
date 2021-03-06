﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace NewsForum.View.Converters
{
    class IsDeleteMessageConverter : IValueConverter
    {

        private string deleteIcon = "";
        private string addIcon = "";


        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (bool)value ? addIcon : deleteIcon;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}