using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenEconomy.Core;
using GreenEconomy.Core.Services;
using GreenEconomy.Core.ViewModels;
using GreenEconomy.Forms.Views;
using Xamarin.Forms;
using DryIoc;
using GreenEconomy.Core.Models;

namespace GreenEconomy.Forms.Services
{
    public class NavigationService : INavigationService
    {
        public NavigationService()
        {
        }
        Dictionary<Type, Type> Pages = new Dictionary<Type, Type>();
        public async Task<T> OpenPageAsync<T>(params BaseModel[] parameters) where T : ViewModelBase
        {
            var pageType = Pages[typeof(T)];
            var page = Activator.CreateInstance(pageType) as ContentPage;
            var vm = IOC.Current.Container.Resolve<T>();
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
            IOC.Current.Container.Register(viewModel);
        }

        public async Task<ViewModelBase> GoBackAsync()
        {
            return (await Shell.Current.Navigation.PopAsync()).BindingContext as ViewModelBase;

        }
    }
}
