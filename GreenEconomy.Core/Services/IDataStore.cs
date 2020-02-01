using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GreenEconomy.Core.Services
{
    public interface IDataStore<T>
    {
        Task<bool> SaveItemAsync(T item);
        Task<bool> DeleteItemAsync(string id);
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
        Task<IEnumerable<T>> SeedItemsAsync(bool forceRefresh = false);
    }
}
