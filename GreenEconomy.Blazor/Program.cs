using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor.Hosting;
using GreenEconomy.Core.Services;
using GreenEconomy.Core.ViewModels;
using Xamarin.Essentials.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using GreenEconomy.Core;


namespace GreenEconomy.Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault();

            builder.RootComponents.Add<App>("app");

            var host = builder.Build();

            builder.Services.AddSingleton<IOC>();
            builder.Services.AddSingleton<INavigationService, NavigationService>();


            Xamarin.Essentials.Blazor.GoogleHttpGeocoding.APIKey = Core.Helpers.Keys.GoogleGeocodingKey;
            builder.Services.AddScoped<IGeocoding, Xamarin.Essentials.Blazor.GoogleHttpGeocoding>();

            //Register pages for navigation
            var nav = host.Services.GetService<INavigationService>();
            nav.Register(typeof(BusinessDetailsViewModel), "businessdetails");
            nav.Register(typeof(BusinessViewModel), "businesses");
            nav.Register(typeof(HomeViewModel), "/");




            await host.RunAsync();
        }
    }
}
