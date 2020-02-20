using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using GreenEconomy.Core;
using GreenEconomy.Core.Services;
using GreenEconomy.Core.ViewModels;
using Microsoft.AspNetCore.Components;
using MvvmHelpers;
using GreenEconomy.Core.Models;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace GreenEconomy.Blazor
{
    public class NavigationService : INavigationService
    {
        public NavigationManager NavigationManager { get; set; }

        Stack<(string url, ViewModelBase vm)> NavigationStack = new Stack<(string url, ViewModelBase vm)>();
        Dictionary<Type, string> Pages = new Dictionary<Type, string>();
        Dictionary<string, ViewModelBase> VMInstances = new Dictionary<string, ViewModelBase>();

        IServiceCollection Collection;
        public NavigationService(IServiceCollection collection)
        {
            Collection = collection;
        }


        public Task<T> OpenPageAsync<T>(params BaseModel[] parameters) where T : ViewModelBase
        {
            var vm = IOC.Current.Provider.GetService(typeof(T)) as ViewModelBase;

            var page = Pages[typeof(T)];
            vm.BaseInit(parameters);

            if (parameters.Any(x => x != null))
            {
                var vmId = parameters.FirstOrDefault().Id;
                VMInstances[vmId] = vm;
                NavigationManager.NavigateTo($"{page}/{vmId}");
            }
            else
            {
                NavigationManager.NavigateTo(page);
            }

            return Task.FromResult(vm as T);
        }


        //I don't like this but I can't seem to find a way to get the VM into the page and keep an instance of the VM here
        public T GetViewModel<T>(string id) where T : ViewModelBase
        {
            ViewModelBase vm;

            if (!string.IsNullOrWhiteSpace(id) && VMInstances.ContainsKey(id)) 
            {

                vm = VMInstances[id] as T;
            }
            else if (!string.IsNullOrWhiteSpace(id))
            {
                vm = IOC.Current.Provider.GetService<T>();    //IOC.Current.Container.Resolve<T>();
                vm.BaseInit(id);
            }
            else
            {
                vm = IOC.Current.Provider.GetService<T>();    //IOC.Current.Container.Resolve<T>();
                vm.BaseInit();
            }

            NavigationStack.Push((NavigationManager.Uri.ToString(), vm));
            return vm as T;
        }

        public void Register(Type viewModel, object page)
        {
            if (! (page is string uri))
                throw new Exception("Invalid page key");
            Pages.Add(viewModel, uri);
            Collection.AddTransient(viewModel);

        }


        /// <summary>
        /// Navigates to previous page in stack. CAUTION Stack may be empty. This method will return null and perform no navigation if there's no page to navigate to
        /// TODO Contemplace going to earlier part of URL Stack
        /// </summary>
        /// <returns></returns>
        public Task<ViewModelBase> GoBackAsync()
        {
            if (NavigationStack.Count < 2)
                return null;

            var currentPage = NavigationStack.Pop();
            var previousPage = NavigationStack.Peek();
            NavigationManager.NavigateTo(previousPage.url);
            return Task.FromResult(previousPage.vm);
        }

        
    }
}
