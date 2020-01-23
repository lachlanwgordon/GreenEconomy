using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using GreenEconomy.Core.Models;
using GreenEconomy.Core.Services;
using MvvmHelpers;
using MvvmHelpers.Commands;

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

        public ICommand NewBusinessCommand => new AsyncCommand(OpenNewBusinessPage);

        public async Task OpenNewBusinessPage()
        {
            Debug.WriteLine($"Nav service is {NavigationService}");
            await NavigationService.OpenPageAsync<BusinessDetailsViewModel>();
        }
    }
}
