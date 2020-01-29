using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using GreenEconomy.Core;
using GreenEconomy.Core.Services;
using GreenEconomy.Core.ViewModels;
using Microsoft.AspNetCore.Components;
using DryIoc;
using MvvmHelpers;

namespace GreenEconomy.Blazor
{
    public class NavigationService : INavigationService
    {
        public NavigationManager NavigationManager { get; set; }

        Stack<(string url, ViewModelBase vm)> NavigationStack = new Stack<(string url, ViewModelBase vm)>();
        Dictionary<Type, string> Pages = new Dictionary<Type, string>();
        Dictionary<Guid, ViewModelBase> VMInstances = new Dictionary<Guid, ViewModelBase>();
 
        public Task<T> OpenPageAsync<T>() where T : ViewModelBase
        {
            var vm = IOC.Current.Container.Resolve(typeof(T)) as ViewModelBase;
            var vmId = Guid.NewGuid();
            VMInstances.Add(vmId, vm);
            Debug.WriteLine($"Nav service adding vm {vmId} {vm}");

            var page = Pages[typeof(T)];
            //NavigationStack.Push((page,vm));
            NavigationManager.NavigateTo(page);
            return Task.FromResult(vm as T);
        }

        //I don't like this but I can't seem to find a way to get the VM into the page and keep an instance of the VM here
        public T GetViewModel<T>(Guid id) where T : ViewModelBase
        {
            Debug.WriteLine($"Nav service getvm for id");
            ViewModelBase vm; 

            if(id != Guid.Empty)
            {
                Debug.WriteLine($"Nav service getvm returning {VMInstances[id]}");

                vm = VMInstances[id] as T;
            }
            else
            {
                Debug.WriteLine($"Nav service getvm resolving a new VM");
                vm = IOC.Current.Container.Resolve<T>();

            }

            vm.BaseInit();
            NavigationStack.Push((NavigationManager.Uri.ToString(), vm));
            Debug.WriteLine($"added {NavigationStack.Peek()} to stack new count {NavigationStack.Count}");
            return vm as T;
        }

        public void Register(Type viewModel, object page)
        {
            if (! (page is string uri))
                throw new Exception("Invalid page key");
            Pages.Add(viewModel, uri);
            IOC.Current.Container.Register(viewModel);

        }

        public Task<ViewModelBase> GoBackAsync()
        {

            var currentPage = NavigationStack.Pop();
            Debug.WriteLine($"Going back to {NavigationStack.Peek()} new count = {NavigationStack.Count}");
            var previousPage = NavigationStack.Peek();
            NavigationManager.NavigateTo(previousPage.url);
            return Task.FromResult(previousPage.vm);
        }
    }
}
