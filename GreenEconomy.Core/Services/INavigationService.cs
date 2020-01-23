using System;
using System.Threading.Tasks;
using GreenEconomy.Core.ViewModels;
using MvvmHelpers;

namespace GreenEconomy.Core.Services
{
    public interface INavigationService
    {
        Task OpenPageAsync<T>(string parameter) where T : ViewModelBase;
        Task OpenPageAsync<T>() where T : ViewModelBase;
        Task GoBackAsync();

        void Register(Type viewMode, object page);
    }
}
