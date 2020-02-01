using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GreenEconomy.Core.Helpers;
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

        public List<string> BusinessTypeNames
        {
            get
            {
                return Enum.GetNames(typeof(BusinessTypes)).Select(b => b.SplitCamelCase()).ToList();
            }
        }

        public List<string> StatusNames
        {
            get
            {
                return Enum.GetNames(typeof(Status)).Select(b => b.SplitCamelCase()).ToList();
            }
        }


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
                IsNewBusiness = true;
                business = new Business { Status = Status.Draft };
                Business = business;
            }
            OnPropertyChanged(nameof(Business));

        }

        public ICommand SaveCommand => new AsyncCommand(Save);

        public async Task Save()
        {
            await DataStore.SaveItemAsync(Business);

            await NavigationService.GoBackAsync();
        }
    }
}
