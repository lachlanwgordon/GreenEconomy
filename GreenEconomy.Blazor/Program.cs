using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor.Hosting;
using DryIoc;
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

            builder.Services.AddSingleton<IOC>();

            var host = builder.Build();

            var ioc = host.Services.GetRequiredService<IOC>();

            ioc.Container.Register<INavigationService, NavigationService>(Reuse.Singleton);

            Xamarin.Essentials.Blazor.GoogleHttpGeocoding.APIKey = GreenEconomy.Core.Helpers.Keys.GoogleGeocodingKey;
            ioc.Container.Register<IGeocoding, Xamarin.Essentials.Blazor.GoogleHttpGeocoding>();

            //Register pages for navigation
            var nav = ioc.Container.Resolve<INavigationService>();
            nav.Register(typeof(BusinessDetailsViewModel), "businessdetails");
            nav.Register(typeof(BusinessViewModel), "businesses");
            nav.Register(typeof(HomeViewModel), "/");


            await builder.Build().RunAsync();
        }
    }
}
