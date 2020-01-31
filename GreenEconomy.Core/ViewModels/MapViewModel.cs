using System;
using System.Threading.Tasks;
using System.Windows.Input;
using GreenEconomy.Core.Models;
using GreenEconomy.Core.Services;
using MvvmHelpers;
using MvvmHelpers.Commands;

namespace GreenEconomy.Core.ViewModels
{
    public class MapViewModel : ViewModelBase
    {
        private readonly IDataStore<Business> BusinessStore;
        public ObservableRangeCollection<Business> Businesses { get; set; } = new ObservableRangeCollection<Business>();
        public MapViewModel(INavigationService navigationService, IDataStore<Business> businessStore) : base(navigationService)
        {
            BusinessStore = businessStore;
        }


        public ICommand RefreshCommand => new AsyncCommand(Refresh);
        public async Task Refresh()
        {
            if (!IsBusy)
                IsBusy = true;

            Businesses.Clear();
            var businesses = await BusinessStore.GetItemsAsync();

            Businesses.AddRange(businesses, System.Collections.Specialized.NotifyCollectionChangedAction.Reset);
            IsBusy = false;
        }

        public override async Task OnAppearingAsync()
        {
            await Refresh();
        }

    }
}
