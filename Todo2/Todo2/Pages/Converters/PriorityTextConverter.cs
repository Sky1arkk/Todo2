﻿using System;
using System.Globalization;
using Xamarin.Forms;

namespace Todo2.Pages.Converters
{
    class PriorityTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string priority = "L";
            switch ((int)value)
            {
                case 0:
                    priority = "L";
                    break;
                case 1:
                    priority = "M";
                    break;
                case 2:
                    priority = "H";
                    break;
            }
            return priority;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
