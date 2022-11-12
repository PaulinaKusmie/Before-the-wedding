using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Before_the_wedding.Services
{
    public interface IPassValue<T>
    {
        Task PassValueAsync(T item);
    }
}
