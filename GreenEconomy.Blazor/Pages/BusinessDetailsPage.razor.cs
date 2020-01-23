using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreenEconomy.Core;
using GreenEconomy.Core.ViewModels;
using DryIoc;

namespace GreenEconomy.Blazor.Pages
{

    public class BusinessDetailsBase : BasePage
    {
        public BusinessDetailsViewModel ViewModel;

        public BusinessDetailsBase()
        {
            ViewModel = IOC.Current.Container.Resolve<BusinessDetailsViewModel>();
        }

    }
}
