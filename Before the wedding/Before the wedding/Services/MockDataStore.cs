using Before_the_wedding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Before_the_wedding.Services
{
    public class MockDataStore //:/// IDataStore<Item>
    {
        

        //public MockDataStore()
        //{
        //    Item itm = new Item();
        //    items = itm.ListDictionary();
        //}

        //public async Task<bool> AddItem(Item item)
        //{
        //    Item itm = new Item();
        //    itm.Add(item.Answear, item.Description);

        //    return await Task.FromResult(true);
        //}

        //public async Task<bool> AddItemAsync(Item item)
        //{
        //    items.Add(item);

        //    return await Task.FromResult(true);
        //}

        //public async Task<bool> UpdateItemAsync(Item item)
        //{
        //    var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
        //    items.Remove(oldItem);
        //    items.Add(item);

        //    return await Task.FromResult(true);
        //}

        //public async Task<bool> DeleteItemAsync(Guid id)
        //{
        //    var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
        //    items.Remove(oldItem);

        //    return await Task.FromResult(true);
        //}

        //public async Task<Item> GetItemAsync(Guid id)
        //{
        //    return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        //}

        //public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        //{
        //    return await Task.FromResult(items);
        //}

        //public Task<bool> DeleteItemAsync(string id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<Item> GetItemAsync(string id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}