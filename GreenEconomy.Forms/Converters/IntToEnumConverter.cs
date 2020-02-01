using System;
using System.Globalization;
using Xamarin.Forms;

namespace GreenEconomy.Forms.Converters
{
    //https://alexdunn.org/2017/05/16/xamarin-tip-binding-a-picker-to-an-enum/
    public class IntToEnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Enum)
            {
                return (int)value;
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
            {
                return Enum.ToObject(targetType, value);
            }
            return 0;
        }
    }
}
