using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using GreenEconomy.Core.Services;
using GreenEconomy.Core.ViewModels;
using Microsoft.AspNetCore.Components;

namespace GreenEconomy.Blazor
{
    public class NavigationService : INavigationService
    {
        public NavigationService(NavigationManager navigationManager)
        {
            NavigationManager = navigationManager;
        }

        public NavigationManager NavigationManager { get; set; }

        static Dictionary<Type, string> Pages = new Dictionary<Type, string>();

        public Task GoBackAsync()
        {
            throw new NotImplementedException();
        }

        public Task OpenPageAsync<T>(string parameter) where T : ViewModelBase
        {
            var page = Pages[typeof(T)];

            NavigationManager.NavigateTo(page);
            return Task.CompletedTask;
        }

        public Task OpenPageAsync<T>() where T : ViewModelBase
        {
            Debug.WriteLine($"open page  pages {Pages.Count}");

            var page = Pages[typeof(T)];
            Debug.WriteLine($"open page  pages {page}");
            Debug.WriteLine($"nav {NavigationManager}");

            NavigationManager.NavigateTo(page);
            return Task.CompletedTask;
        }

        public static void Register(Type vm, object page)
        {
            Debug.WriteLine($"Registering page {vm} {page}");
            if (! (page is string uri))
                throw new Exception("Invalid page key");
            Pages.Add(vm, uri);
            Debug.WriteLine($"Registered pages {Pages.Count}");

        }
    }
}
