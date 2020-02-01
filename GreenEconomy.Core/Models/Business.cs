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

        public string Address { get; set; }
        public string ClimateAction { get; set; }
        public string Website {get;set;}
        public BusinessTypes BusinessType { get; set; } = BusinessTypes.Unknown;
        public Status Status { get; set; } = Status.Pending;

        public Business()
        {
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
    }
}
