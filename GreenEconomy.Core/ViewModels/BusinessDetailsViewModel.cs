using System;
using System.Threading.Tasks;
using System.Windows.Input;
using GreenEconomy.Core.Services;
using MvvmHelpers.Commands;

namespace GreenEconomy.Core.ViewModels
{
    public class BusinessDetailsViewModel : ViewModelBase
    {
        public BusinessDetailsViewModel(INavigationService navigationService) : base( navigationService)
        {
        }

        public ICommand BackCommand => new AsyncCommand(GoBack);

        public async Task GoBack()
        {
            await NavigationService.GoBackAsync();
        }
    }
}
