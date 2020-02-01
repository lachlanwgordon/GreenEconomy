using System;
using System.Diagnostics;
using Newtonsoft.Json;
using Xamarin.Essentials;

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

        public Business()
        {
        }
    }
}
