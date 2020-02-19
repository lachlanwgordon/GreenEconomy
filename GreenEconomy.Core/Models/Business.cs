using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace GreenEconomy.Core.Models
{
    public class Business : BaseModel
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        [JsonIgnore]
        public Location Location { get; set; } = new Location();
        public double Latitude { get => Location.Latitude; set => Location.Latitude = value; }
        public double Longitude { get => Location.Longitude; set => Location.Longitude = value; }

        public string Address { get; set; }
        public string ClimateAction { get; set; }
        public string Website {get;set;}
        public BusinessTypes BusinessType { get; set; }
        public Status Status { get; set; }

        public Business()
        {
        }

        public async Task<IEnumerable<Location>> Geocode()
        {
            try
            {
                var geocode = IOC.Current.Provider.GetService<Xamarin.Essentials.Interfaces.IGeocoding>();

                var locations = await geocode.GetLocationsAsync(Address);
                if(Location == null || Latitude == 0 && Longitude == 0)
                {
                    Location = locations.FirstOrDefault();
                }
                return locations;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"cannot geocode probably running on unsupported platofrm \n {ex}");
            }
            return null;
        }
        
    }
    public enum BusinessTypes
    {
        Unknown = 0,
        Cafe = 1,
        Restaurant = 2,
        Bar = 3,
        Grocery = 4,
    }
    public enum Status
    {
        Unknown = 0,
        Pending = 1,
        Approved = 2,
        Rejected = 3,
        Draft = 4,
    }
}
