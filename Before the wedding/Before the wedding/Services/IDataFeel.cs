using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Before_the_wedding.Services
{
    public interface IDataFeel<T>
    {
        #region Feel
        Task<Feel> FetchFeelItem();
        Task<bool> SaveOrEditFeelItem(T item);
        #endregion

    }
}
