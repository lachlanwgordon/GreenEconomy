using System;
using System.Diagnostics;
using System.Threading.Tasks;
using GreenEconomy.Core.ViewModels;
using Microsoft.AspNetCore.Components;

namespace GreenEconomy.Blazor.Pages
{
    public class BasePage : ComponentBase
    {
        [Inject] NavigationManager NavigationManager { get; set; }
        public ViewModelBase ViewModelBase { get; set; }

        public BasePage()
        {
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            Debug.WriteLine($"Nav is {NavigationManager} in base");
            ViewModelBase.NavigationService = new NavigationService(NavigationManager);
        }
    }
}
