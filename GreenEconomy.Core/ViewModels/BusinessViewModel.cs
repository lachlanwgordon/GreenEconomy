using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using GreenEconomy.Core.Models;
using GreenEconomy.Core.Services;
using MvvmHelpers;
using MvvmHelpers.Commands;
using DryIoc;
using System.Collections.Generic;
using System.Linq;
using GreenEconomy.Core.Helpers;

namespace GreenEconomy.Core.ViewModels
{
    public class BusinessViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Business> Businesses { get; set; } = new ObservableRangeCollection<Business>();

        readonly IDataStore<Business> BusinessStore;

        


        public BusinessViewModel(INavigationService navigationService, IDataStore<Business> dataStore) : base(navigationService)
        {
            BusinessStore = dataStore;
        }

        public ICommand EditBusinessCommand => new AsyncCommand<Business>(EditBusiness);
        public async Task EditBusiness(Business business = null)
        {
            await NavigationService.OpenPageAsync<BusinessDetailsViewModel>(business);
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

        public List<string> StatusNames
        {
            get
            {
                return Enum.GetNames(typeof(Status)).Select(b => b.SplitCamelCase()).ToList();
            }
        }

        public override async Task OnAppearingAsync()
        {
            await Refresh();
        }
    }
}
