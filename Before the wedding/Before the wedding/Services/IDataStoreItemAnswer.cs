using Before_the_wedding.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Before_the_wedding.Services.Models
{
    public interface IDataStoreItemAnswer<T>
    {
        Task<bool> EditItemAnswerAsync(T itemAnswer);
        Task<bool> AddItemAnswerAsync(T item);
        Task<ItemAnswer> NewEditItemAnswer(T item);
        Task<List<ItemAnswer>> LoadingItemAnswerAsync(T item);
        Task<List<ItemAnswer>> LoadingItemAnswerAsync(Item item);
       
    }
}
