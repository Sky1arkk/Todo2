using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Todo2.Pages.Converters
{
    class PriorityLabelColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color priority = Color.Gray;
            switch ((int)value)
            {
                case 0:
                    priority = Color.FromHex("#90EE90");
                    break;
                case 1:
                    priority = Color.FromHex("#FFEB3B");
                    break;
                case 2:
                    priority = Color.FromHex("#F44336");
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
