using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Before_the_wedding.Services
{
    public interface IDataValue<T>
    {
        #region Value
        Task<Value> FetchValueItem();
        Task<bool> SaveOrEditValueItem(T item);
        #endregion

    }
}
