using System;
using System.Threading.Tasks;
using System.Windows.Input;
using GreenEconomy.Core.Services;
using MvvmHelpers.Commands;

namespace GreenEconomy.Core.ViewModels
{
    public class BusinessDetailsViewModel : ViewModelBase
    {
        public BusinessDetailsViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public ICommand SaveCommand => new AsyncCommand(Save);

        public async Task Save()
        {
            await NavigationService.GoBackAsync();
        }
    }
}
