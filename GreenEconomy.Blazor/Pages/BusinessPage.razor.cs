using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenEconomy.Core;
using GreenEconomy.Core.Models;
using GreenEconomy.Core.Services;
using Microsoft.AspNetCore.Components;
using DryIoc;
using MvvmHelpers;
using GreenEconomy.Core.ViewModels;
using System.Diagnostics;

namespace GreenEconomy.Blazor.Pages
{
    public class BusinessPageBase : BasePage
    {
        public BusinessViewModel ViewModel;

        public BusinessPageBase()
        {
            ViewModel = IOC.Current.Container.Resolve<BusinessViewModel>();
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await ViewModel.OnInitalizeAsync();
        }

    }
}
