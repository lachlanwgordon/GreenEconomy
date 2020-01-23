using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GreenEconomy.Core.Services;
using GreenEconomy.Core.ViewModels;
using Microsoft.AspNetCore.Components;

namespace GreenEconomy.Blazor
{
    public class NavigationService : INavigationService
    {
        public NavigationService()
        {
        }

        NavigationManager navigationManager;
        public  NavigationManager NavigationManager
        {
            get => navigationManager;
            internal set
            {
                navigationManager = value;
                UpdateStack();
            }
        }

        void UpdateStack()
        {
            //NavigationStack.Add(NavigationManager.Uri);
            //PrintStack("stack updated");

        }

        //static List<string> NavigationStack = new List<string>();
        Dictionary<Type, string> Pages = new Dictionary<Type, string>();

        public Task GoBackAsync()
        {
            //PrintStack("Start Go back");



            //if (NavigationStack.Count > 1)
            //{
            //    var page = NavigationStack.ElementAtOrDefault(NavigationStack.Count - 1);
            //    NavigationStack.RemoveAt(NavigationStack.Count);
            //    NavigationStack.RemoveAt(NavigationStack.Count);
            //    Debug.WriteLine($"Navigate back to {page} new stack new size {NavigationStack.Count}");

            //    NavigationManager.NavigateTo(page);
            //}
            //else
            //{
            //    Debug.WriteLine($"Theres's nothing on the stack new size {NavigationStack.Count}");

            //}
            //PrintStack("finishe Go back");
                NavigationManager.NavigateTo("businesses");

            return Task.CompletedTask;
        }

        //private void PrintStack(string message)
        //{
        //    Debug.WriteLine(message);
        //    foreach (var url in NavigationStack)
        //    {
        //        Debug.WriteLine(url);
        //    }
        //}

        public Task OpenPageAsync<T>(string parameter) where T : ViewModelBase
        {
            //PrintStack("about to open page with parameter");

            //var page = Pages[typeof(T)];
            //NavigationStack.Add(page);
            //Debug.WriteLine($"Added {page} to stack new size {NavigationStack.Count}");
            //NavigationManager.NavigateTo(page);
            //PrintStack("Finished navigating too with parameter");

            return Task.CompletedTask;
        }

        public Task OpenPageAsync<T>() where T : ViewModelBase
        {
            //PrintStack("about to open page no parameter");
            //var page = Pages[typeof(T)];
            //NavigationStack.Add(page);

            NavigationManager.NavigateTo("businessdetails");
            //PrintStack("Finished navigating too no parameter");

            return Task.CompletedTask;
        }

        public void Register(Type viewModel, object page)
        {
            Debug.WriteLine($"Registering page {viewModel} {page}");
            if (!(page is string uri))
                throw new Exception("Invalid page key");
            Pages.Add(viewModel, uri);
            Debug.WriteLine($"Registered pages {Pages.Count}");

        }
    }
}
