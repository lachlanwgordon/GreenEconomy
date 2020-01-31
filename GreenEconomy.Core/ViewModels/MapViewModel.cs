using System;
using System.Threading.Tasks;
using System.Windows.Input;
using GreenEconomy.Core.Models;
using GreenEconomy.Core.Services;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Xamarin.Essentials;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;

namespace GreenEconomy.Core.ViewModels
{
    public class MapViewModel : ViewModelBase
    {
        private readonly IDataStore<Business> BusinessStore;
        private readonly IGeolocation Geolocation;

        public ObservableRangeCollection<Business> Businesses { get; set; } = new ObservableRangeCollection<Business>();
        public Location CurrentLocation { get; set; }


        public MapViewModel(INavigationService navigationService, IDataStore<Business> businessStore, IGeolocation geolocation) : base(navigationService)
        {
            BusinessStore = businessStore;
            Geolocation = geolocation;
        }
        public string SearchText { get; set; } = "hello";

        public ICommand RefreshCommand => new AsyncCommand(Refresh);
        public async Task Refresh()
        {
            if (!IsBusy)
                IsBusy = true;

            Businesses.Clear();
            var businesses = await BusinessStore.GetItemsAsync();
            Businesses.AddRange(businesses);

            CurrentLocation = await Geolocation.GetLocationAsync();

            IsBusy = false;
        }

        public ICommand SearchCommand => new AsyncCommand(Search);

        private async Task Search()
        {
            var businesses = await BusinessStore.GetItemsAsync();

            Businesses.AddRange(businesses);
        }

        public override async Task OnAppearingAsync()
        {
            await Refresh();
        }

    }
}
