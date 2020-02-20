using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor.Hosting;
using GreenEconomy.Core.Services;
using GreenEconomy.Core.ViewModels;
using Xamarin.Essentials.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using GreenEconomy.Core;
using System.Diagnostics;

namespace GreenEconomy.Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault();

            builder.RootComponents.Add<App>("app");

            var host = builder.Build();

            builder.Services.AddSingleton<IOC>(new IOC(builder.Services));


            Xamarin.Essentials.Blazor.GoogleHttpGeocoding.APIKey = Core.Helpers.Keys.GoogleGeocodingKey;
            builder.Services.AddScoped<IGeocoding, Xamarin.Essentials.Blazor.GoogleHttpGeocoding>();

            var nav = new NavigationService(builder.Services);
            builder.Services.AddSingleton<INavigationService>(nav);

            //Register pages for navigation
            nav.Register(typeof(BusinessDetailsViewModel), "businessdetails");
            nav.Register(typeof(BusinessViewModel), "businesses");
            nav.Register(typeof(HomeViewModel), "/");


            var provider = builder.Services.BuildServiceProvider();
            Debug.WriteLine($"Provider built   ");
            IOC.Current.Provider = provider;

            foreach (var service in builder.Services)
            {
                Debug.WriteLine($"service: {service.ImplementationInstance} {service.ImplementationType} {service.Lifetime}");
            }

            Debug.WriteLine($"services loaded");

            var na = provider.GetService<INavigationService>();
            Debug.WriteLine($"nav: {na}");

            await host.RunAsync();
        }
    }
}
