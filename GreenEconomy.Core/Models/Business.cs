using System;
using System.Diagnostics;
using Xamarin.Essentials;

namespace GreenEconomy.Core.Models
{
    public class Business : BaseModel
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public Location Location { get; set; }

        public Business()
        {
        }
    }
}
