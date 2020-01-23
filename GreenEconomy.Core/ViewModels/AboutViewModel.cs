using System;
using System.Windows.Input;
using MvvmHelpers.Commands;
using Xamarin.Essentials;

namespace GreenEconomy.Core.ViewModels
{
    public class AboutViewModel : ViewModelBase
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://xamarin.com"));
        }

        public ICommand OpenWebCommand { get; }
    }
}