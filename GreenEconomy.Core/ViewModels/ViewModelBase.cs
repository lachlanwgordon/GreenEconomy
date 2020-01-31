using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using GreenEconomy.Core.Services;
using MvvmHelpers.Commands;

namespace GreenEconomy.Core.ViewModels
{
    public class ViewModelBase : MvvmHelpers.BaseViewModel
    {
        public INavigationService NavigationService { get; set; }
        public string Error { get; set; }

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public void BaseInit(params object[] parameters)
        {
            OnInitialize(parameters);
            _ = OnIntializeAsync(parameters);//TODO Fix this so it's sot async voide? or is this ok?
        }

        public virtual Task OnAppearingAsync()
        {
            return Task.CompletedTask;
        }

        public virtual void OnInitialize(params object[] parameters)
        {

        }

        public virtual Task OnIntializeAsync(params object[] parameters)
        {
            return Task.CompletedTask;
        }

        public ICommand BackCommand => new AsyncCommand(GoBack);
        public async Task GoBack()
        {
            await NavigationService.GoBackAsync();
        }
    }
}
