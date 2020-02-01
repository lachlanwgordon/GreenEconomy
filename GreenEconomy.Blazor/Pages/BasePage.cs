using System;
using System.Diagnostics;
using System.Threading.Tasks;
using GreenEconomy.Core;
using GreenEconomy.Core.Services;
using GreenEconomy.Core.ViewModels;
using Microsoft.AspNetCore.Components;
using DryIoc;
using System.Net.Http;

namespace GreenEconomy.Blazor.Pages
{
    public class BasePage<T> : ComponentBase where T : ViewModelBase
    {
        [Parameter]
        public string id { get; set; }

        [Inject] NavigationManager NavigationManager { get; set; }
        public T ViewModel;

        [Inject] HttpClient HttpClient { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            var nav = IOC.Current.Container.Resolve<INavigationService>() as NavigationService;
            nav.NavigationManager = NavigationManager;

            IOC.Current.Container.RegisterInstance(HttpClient);

            ViewModel = nav.GetViewModel<T>(id);
            await ViewModel.OnAppearingAsync();

        }
    }
}
