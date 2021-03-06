﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GreenEconomy.Core.Models;
using Xamarin.Essentials;

namespace GreenEconomy.Core.Services
{
    public class BusinessStore : IDataStore<Business>
    {
        private readonly IWebClient WebClient;

        public BusinessStore(IWebClient webClient)
        {
            WebClient = webClient;
        }

        List<Business> Businesses { get; set; } = new List<Business>
        {
            new Business{Name = "Lachlan's Cafe", Location = new Location(-37.5, 144.9), PhoneNumber = "156888", Id =  "8a5982a3-df0b-4dfb-b76b-48fe041ce67a" },
            new Business{Name = "Bike Shop", Location = new Location(-37.3, 144.65), PhoneNumber = "1256888", Id = "ff5982a3-df0b-4dfb-b76b-48fe041ce67a" }
        };



        public Task<bool> DeleteItemAsync(string id)
        {
            var remove = Businesses.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(Businesses.Remove(remove));
        }

        public Task<Business> GetItemAsync(string id)
        {
            var item = Businesses.FirstOrDefault(x => x.Id.ToLower() == id.ToLower());
            foreach (var bus in Businesses)
            {
                Debug.WriteLine($"looking for: {id}");
                Debug.WriteLine($"This item:   {bus.Id}");
                Debug.WriteLine($"match:       {bus.Id == id}\n");
            }


            Debug.WriteLine($"Found Item {item}");
            return Task.FromResult(item);
        }

        public async Task<IEnumerable<Business>> GetItemsAsync(bool forceRefresh = false)
        {
            var items = await WebClient.GetAsync<Business>();
            return items;
        }

        public Task<IEnumerable<Business>> SeedItemsAsync(bool forceRefresh = false)
        {
            return Task.FromResult<IEnumerable<Business>>(Businesses.ToList());
        }

        public async Task<bool> SaveItemAsync(Business item)
        {
            await WebClient.PostAsync<Business>(item);

            return true;
        }
    }
}
