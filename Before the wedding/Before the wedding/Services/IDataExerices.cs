using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Before_the_wedding.Services
{
    public interface IDataExerices<T>
    {
        #region Letter
        Task<Letter> FetchLetterItem();
        Task<bool> SaveOrEditLetterItem(T item);
        #endregion

        #region Feel
        Task<Feel> FetchFeelItem();
        Task<bool> SaveOrEditFeelItem(T item);
        #endregion

        #region Value
        Task<Value> FetchValueItem();
        Task<bool> SaveOrEditValueItem(T item);
        #endregion






    }
}
