using System;
using System.Globalization;
using GreenEconomy.Forms.Converters;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace GreenEconomy.Forms.Converters
{
    [ValueConversion(typeof(Location), typeof(CameraUpdate))]
    public class LocationToMapCameraUpdateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Location == false)
            {
                return CameraUpdateFactory.NewCameraPosition(new CameraPosition(new Position(-38, 145), 8));
            }

            var input = (Location)value;

            var camera = CameraUpdateFactory.NewCameraPosition(new CameraPosition(new Position(input.Latitude, input.Longitude), 8));


            return camera;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}