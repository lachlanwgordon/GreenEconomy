using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenEconomy.Core;
using GreenEconomy.Core.Services;
using GreenEconomy.Core.ViewModels;
using GreenEconomy.Forms.Views;
using Xamarin.Forms;
using GreenEconomy.Core.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace GreenEconomy.Forms.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IServiceCollection Collection;
        public NavigationService(IServiceCollection services)
        {
            Collection = services;
        }
        Dictionary<Type, Type> Pages = new Dictionary<Type, Type>();

        public async Task<T> OpenPageAsync<T>(params BaseModel[] parameters) where T : ViewModelBase
        {
            var pageType = Pages[typeof(T)];
            var page = Activator.CreateInstance(pageType) as ContentPage;
            var vm = IOC.Current.Provider.GetService<T>();
            page.BindingContext = vm;
            vm.BaseInit(parameters);
            await Shell.Current.Navigation.PushAsync(page);
            return vm;
        }

        public void Register(Type viewModel, object page)
        {
            if(page is Type contentPage)
            {
                Pages.Add(viewModel, contentPage);
            }
            Collection.AddTransient(viewModel);
            Debug.WriteLine($"vm {viewModel} added to services count: {Collection.Count}");
        }

        public async Task<ViewModelBase> GoBackAsync()
        {
            return (await Shell.Current.Navigation.PopAsync()).BindingContext as ViewModelBase;

        }
    }
}
