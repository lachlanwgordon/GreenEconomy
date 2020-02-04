using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor.Hosting;
using DryIoc;
using GreenEconomy.Core.Services;
using GreenEconomy.Core.ViewModels;
using Xamarin.Essentials.Interfaces;

namespace GreenEconomy.Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault();

            var ioc = new GreenEconomy.Core.IOC();
            ioc.Initialize();
            ioc.Container.Register<INavigationService, NavigationService>(Reuse.Singleton);//   .Register<INavigationService>(Reuse.  navService);
            ioc.Container.Register<IGeocoding, Xamarin.EssentialsBL.Blazor.GeocodingImpl>();

            //Register pages for navigation
            var nav = ioc.Container.Resolve<INavigationService>();
            nav.Register(typeof(BusinessDetailsViewModel), "businessdetails");
            nav.Register(typeof(BusinessViewModel), "businesses");
            nav.Register(typeof(HomeViewModel), "/");

            builder.RootComponents.Add<App>("app");
            await builder.Build().RunAsync();
        }

    }
}
