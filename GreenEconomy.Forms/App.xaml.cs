using DryIoc;
using GreenEconomy.Core;
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

        protected override async void OnStart()
        {
            await IOC.Initialize();
            IOC.Container.Register<Xamarin.Essentials.Interfaces.IGeolocation, Xamarin.Essentials.Implementation.GeolocationImplementation>();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
