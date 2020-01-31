using System;
using System.Diagnostics;
using GreenEconomy.Core;
using GreenEconomy.Core.ViewModels;
using Xamarin.Forms;
using DryIoc;

namespace GreenEconomy.Forms.Views
{
    public class BasePage<T> : ContentPage where T : ViewModelBase
    {
        public T ViewModel => BindingContext as T;

        public BasePage()
        {
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if(BindingContext == null)
            {
                BindingContext = IOC.Current.Container.Resolve<T>();
                (BindingContext as T).BaseInit();
            }
            await ViewModel.OnAppearingAsync();
        }
    }
}

