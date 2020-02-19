using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using GreenEconomy.Core;
using GreenEconomy.Core.Services;
using GreenEconomy.Core.ViewModels;
using GreenEconomy.Forms.Services;
using GreenEconomy.Forms.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Xamarin.Forms;

namespace GreenEconomy
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            var services = new ServiceCollection();
            Debug.WriteLine($"services created:   {services.Count}");

            services.AddTransient<Xamarin.Essentials.Interfaces.IGeolocation, Xamarin.Essentials.Implementation.GeolocationImplementation>();
            services.AddSingleton<IOC>(new IOC(services));
            services.AddTransient<Xamarin.Essentials.Interfaces.IGeocoding, Xamarin.Essentials.Implementation.GeocodingImplementation>();
            services.AddSingleton<HttpClient>();

            var nav = new NavigationService(services);
            services.AddSingleton<INavigationService>(nav);


            nav.Register(typeof(BusinessDetailsViewModel), typeof(BusinessDetailsPage));
            nav.Register(typeof(BusinessViewModel), typeof(BusinessPage));
            nav.Register(typeof(MapViewModel), typeof(MapPage));

            Debug.WriteLine($"Nav registered. total:  {services.Count}");

            var provider = services.BuildServiceProvider();
            Debug.WriteLine($"Provider built   ");
            IOC.Current.Provider = provider;
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
