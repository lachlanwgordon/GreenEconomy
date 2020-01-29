using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using GreenEconomy.Core.Services;

namespace GreenEconomy.Core.ViewModels
{
    public class ViewModelBase : MvvmHelpers.BaseViewModel
    {
        public INavigationService NavigationService { get; set; }

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public void BaseInit()
        {
            OnInitialize();
            _ = OnIntializeAsync();//TODO Fix this so it's sot async voide?
        }

        public virtual void OnInitialize()
        {

        }

        public virtual Task OnIntializeAsync()
        {
            return Task.CompletedTask;
        }
    }
}
