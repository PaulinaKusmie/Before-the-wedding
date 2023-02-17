using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Before_the_wedding.Services
{
    public interface IDataLetter<T>
    {
        #region Letter
        Task<Letter> FetchLetterItem();
        Task<bool> SaveOrEditLetterItem(T item);
        #endregion

    }
}
