﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using GreenEconomy.Core.Models;
using GreenEconomy.Core.Services;
using MvvmHelpers;
using MvvmHelpers.Commands;
using DryIoc;

namespace GreenEconomy.Core.ViewModels
{
    public class BusinessViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Business> Businesses { get; set; }

        readonly IDataStore<Business> BusinessStore;


        //This constructor will always fail the second time it runs but will be fine after that
        public BusinessViewModel(INavigationService navigationService, IDataStore<Business> dataStore) : base(navigationService)
        {
            Debug.WriteLine($"Construct business vm");
            BusinessStore = dataStore;
        }


        public override async Task OnIntializeAsync()
        {
            await base.OnIntializeAsync();
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
