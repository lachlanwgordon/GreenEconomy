using GreenEconomy.Core.Services;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using DryIoc;
using GreenEconomy.Core.ViewModels;

namespace GreenEconomy.Blazor
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
            var ioc = new GreenEconomy.Core.IOC();
            ioc.Initialize();



            //Register services that are blazor specific.
            //For shared services go to Core.IOC
            ioc.Container.Register<INavigationService, NavigationService>();




            //Register pages for navigation
            var nav = ioc.Container.Resolve<INavigationService>();
            nav.Register(typeof(BusinessDetailsViewModel), "counter");
        }
    }
}
