using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using GreenEconomy.Core.Services;

namespace GreenEconomy.Core.ViewModels
{
    public class ViewModelBase : MvvmHelpers.BaseViewModel
    {
        public INavigationService NavigationService { get; set; }
    }
}
