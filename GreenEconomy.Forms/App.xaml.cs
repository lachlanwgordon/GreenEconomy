﻿using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using DryIoc;
using GreenEconomy.Core;
using GreenEconomy.Core.Services;
using GreenEconomy.Core.ViewModels;
using GreenEconomy.Forms.Services;
using GreenEconomy.Forms.Views;
using Xamarin.Forms;

namespace GreenEconomy
{
    public partial class App : Application
    {
        public IOC IOC = new IOC();

        public App()
        {
            InitializeComponent();
            
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            IOC.Initialize();

            IOC.Container.Register<Xamarin.Essentials.Interfaces.IGeolocation, Xamarin.Essentials.Implementation.GeolocationImplementation>();
            IOC.Container.Register<INavigationService, NavigationService>(Reuse.Singleton);
            var http = new HttpClient();
            IOC.Container.RegisterInstance<HttpClient>(http);

            var nav = IOC.Container.Resolve<INavigationService>();

            nav.Register(typeof(BusinessDetailsViewModel), typeof(BusinessDetailsPage));
            nav.Register(typeof(BusinessViewModel), typeof(BusinessPage));
            nav.Register(typeof(MapViewModel), typeof(MapPage));
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
