using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Xamarin.Essentials.Blazor
{
    public class Geocoding : Xamarin.Essentials.Interfaces.IGeocoding
    {
        public Geocoding()
        {
        }

        public Task<IEnumerable<Location>> GetLocationsAsync(string address)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Placemark>> GetPlacemarksAsync(Location location)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Placemark>> GetPlacemarksAsync(double latitude, double longitude)
        {
            throw new NotImplementedException();
        }
    }
}
