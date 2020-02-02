using System;
using GreenEconomy.Core.Models;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using GreenEconomy.Core;
using DryIoc;
using GreenEconomy.Core.Services;
using Xamarin.Essentials.Interfaces;

[assembly: FunctionsStartup(typeof(GreenEconomy.Functions.Startup))]
namespace GreenEconomy.Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var business = new Business();
            var ioc = new IOC();
            ioc.Initialize();
            ioc.Container.Register<IGeocoding, Xamarin.Essentials.Blazor.Geocoding>();

        }
    }
}
