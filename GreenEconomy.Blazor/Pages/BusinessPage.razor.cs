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
    public class BusinessPageBase : ComponentBase
    {
        public BusinessViewModel ViewModel;

        protected override async Task OnInitializedAsync()
        {
            ViewModel = IOC.Current.Container.Resolve<BusinessViewModel>();
            await ViewModel.OnInitalizeAsync();
        }
    }
}
