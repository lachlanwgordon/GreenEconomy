using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GreenEconomy.Core.Models;
using GreenEconomy.Core.Services;
using MvvmHelpers.Commands;

namespace GreenEconomy.Core.ViewModels
{
    public class BusinessDetailsViewModel : ViewModelBase
    {
        private IDataStore<Business> DataStore;

        public BusinessDetailsViewModel(INavigationService navigationService, IDataStore<Business> dataStore) : base( navigationService)
        {
            DataStore = dataStore;
        }

        public Business Business { get; private set; }
        public bool IsNewBusiness { get; private set; }

        public override async Task OnIntializeAsync(params object[] parameters)
        {
            await base.OnIntializeAsync(parameters);
            if (parameters.FirstOrDefault(x => x is Business) is Business business)
            {
                Business = business;
            }
            else if (parameters.FirstOrDefault(x => x is string) is string businessId)
            {
                business = await DataStore.GetItemAsync(businessId);
                if (business == null)
                {
                    Debug.WriteLine("Error");
                    Error = "Business not found";
                }
                else
                {
                    Business = business;
                }
            }
            else
            {
                Business = new Business();
                IsNewBusiness = true;
            }
            OnPropertyChanged(nameof(Business));

        }

        public ICommand SaveCommand => new AsyncCommand(Save);

        public async Task Save()
        {
            if (IsNewBusiness)
                await DataStore.AddItemAsync(Business);
            else
                await DataStore.UpdateItemAsync(Business);
            await NavigationService.GoBackAsync();
        }
    }
}
