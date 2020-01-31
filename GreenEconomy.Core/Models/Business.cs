using System;
using System.Diagnostics;

namespace GreenEconomy.Core.Models
{
    public class Business : BaseModel
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Business()
        {
            Debug.WriteLine("Constructing a business");
        }
    }
}
