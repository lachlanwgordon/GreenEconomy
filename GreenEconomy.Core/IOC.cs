using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using GreenEconomy.Core.Models;
using GreenEconomy.Core.Services;
using GreenEconomy.Core.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace GreenEconomy.Core
{
    public class IOC
    {
        public IServiceProvider Provider;
        public readonly IServiceCollection Collection;
        public IOC(IServiceCollection collection)
        {

            Collection = collection;

            collection.AddScoped<IWebClient, WebClient>();
            collection.AddScoped<IDataStore<Business>, BusinessStore>();

            Current = this;
        }

        public static IOC Current { get; private set; }
    }
}
