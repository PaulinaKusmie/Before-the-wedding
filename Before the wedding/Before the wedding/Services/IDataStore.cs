using Before_the_wedding.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Before_the_wedding.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> EditItemAsync(T item);
        Task<bool> DeleteItemAsync(Guid id);

        Task<List<Item>> LoadingItemAsync();
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);

    }
}
