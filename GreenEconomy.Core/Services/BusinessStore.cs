using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreenEconomy.Core.Models;

namespace GreenEconomy.Core.Services
{
    public class BusinessStore : IDataStore<Business>
    {
        public BusinessStore()
        {
        }

        List<Business> Businesses { get; set; } = new List<Business>
        {
            new Business{Name = "Lachlan's Cafe", Latitude = -37.5, Longitude = 144.9, PhoneNumber = "156888", Id =  Guid.NewGuid().ToString() },
            new Business{Name = "Bike Shop", Latitude = -37.3, Longitude = 144.65, PhoneNumber = "1256888", Id = Guid.NewGuid().ToString() }
        };

        public Task<bool> AddItemAsync(Business item)
        {
            if(string.IsNullOrWhiteSpace(item.Id))
            {
                item.Id = Guid.NewGuid().ToString();
            }
            Businesses.Add(item);
            return Task.FromResult(true);
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            var remove = Businesses.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(Businesses.Remove(remove));
        }

        public Task<Business> GetItemAsync(string id)
        {
            var item = Businesses.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(item);
        }

        public Task<IEnumerable<Business>> GetItemsAsync(bool forceRefresh = false)
        {
            return Task.FromResult<IEnumerable<Business>>(Businesses.ToList());
        }

        public Task<bool> UpdateItemAsync(Business item)
        {
            var remove = Businesses.FirstOrDefault(x => x.Id == item.Id);

            if (remove != null)
            {
                Businesses.Add(item);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);

        }
    }
}
