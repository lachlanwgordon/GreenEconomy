using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenEconomy.Core;
using GreenEconomy.Core.Models;
using GreenEconomy.Core.Services;
using Microsoft.AspNetCore.Components;
using DryIoc;
using MvvmHelpers;

namespace GreenEconomy.Blazor.Pages
{
    public class FetchDataBase 
    {
        public ObservableRangeCollection<Business> Businesses;


        IDataStore<Business> Store;
        protected override async Task OnInitializedAsync()
        {
            Store = IOC.Current.Container.Resolve<IDataStore<Business>>();

            var businesses = await Store.GetItemsAsync();
            Businesses = new ObservableRangeCollection<Business>();
            Businesses.AddRange(businesses);
        }

        public class WeatherForecast
        {
            public DateTime Date { get; set; }

            public int TemperatureC { get; set; }

            public string Summary { get; set; }

            public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        }
    }
}
