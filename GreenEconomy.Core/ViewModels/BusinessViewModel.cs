using System;
using System.Diagnostics;
using System.Threading.Tasks;
using GreenEconomy.Core.Models;
using GreenEconomy.Core.Services;
using MvvmHelpers;

namespace GreenEconomy.Core.ViewModels
{
    public class BusinessViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Business> Businesses { get; set; }

        readonly IDataStore<Business> BusinessStore;

        public BusinessViewModel(IDataStore<Business> dataStore)
        {
            BusinessStore = dataStore;
        }

        public async Task OnInitalizeAsync()
        {
            var businesses = await BusinessStore.GetItemsAsync();
            Businesses = new ObservableRangeCollection<Business>();
            OnPropertyChanged(nameof(Businesses));
            Businesses.AddRange(businesses);
        }
    }
}
