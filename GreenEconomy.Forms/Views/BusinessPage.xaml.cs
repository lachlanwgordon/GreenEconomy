using System;
using System.Collections.Generic;
using GreenEconomy.Core;
using GreenEconomy.Core.ViewModels;
using Xamarin.Forms;
using DryIoc;

namespace GreenEconomy.Forms.Views
{
    public partial class BusinessPage : ContentPage
    {
        BusinessViewModel ViewModel;
        public BusinessPage()
        {
            InitializeComponent();
            BindingContext = ViewModel = IOC.Current.Container.Resolve<BusinessViewModel>();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ViewModel.OnInitalizeAsync();
        }
    }
}
