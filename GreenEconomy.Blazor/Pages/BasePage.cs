using System;
using System.Diagnostics;
using System.Threading.Tasks;
using GreenEconomy.Core;
using GreenEconomy.Core.ViewModels;
using Microsoft.AspNetCore.Components;
using DryIoc;
using GreenEconomy.Core.Services;
using Microsoft.AspNetCore.Components.Web;

namespace GreenEconomy.Blazor.Pages
{
    public class BasePage : ComponentBase
    {
        [Inject]
        NavigationManager NavigationManager { get; set; }

        public BasePage()
        {
            Debug.WriteLine($"Base page const");

        }

        protected override async Task OnInitializedAsync()
        {
            Debug.WriteLine($"Base page nav init");

            var service = IOC.Current.Container.Resolve<INavigationService>() as NavigationService;
            Debug.WriteLine($"Base page nav resolved service: {service} manager: {NavigationManager}");
            service.NavigationManager = NavigationManager;

            await base.OnInitializedAsync();
        }
    }
}
