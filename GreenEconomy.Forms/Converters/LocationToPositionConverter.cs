using System;
using System.Globalization;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin;
using Xamarin.Forms.GoogleMaps;

namespace GreenEconomy.Forms.Converters
{
    [ValueConversion(typeof(Location), typeof(Position))]
    public class LocationToPositionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Location == false)
            {
                return default(Position);
            }

            var input = (Location)value;

            // TODO: Put your value conversion logic here.
            var pos = new Position(input.Latitude, input.Longitude);
            return pos;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}