using System;
using System.Threading.Tasks;
using System.Windows.Input;
using GreenEconomy.Core.Services;
using MvvmHelpers.Commands;

namespace GreenEconomy.Core.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public HomeViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public ICommand RegisterCommand => new AsyncCommand(Register);

        public async Task Register()
        {
            await NavigationService.OpenPageAsync<BusinessDetailsViewModel>();
        }
    }
}
