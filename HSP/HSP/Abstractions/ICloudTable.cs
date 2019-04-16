using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HSP.Abstractions
{
    /**************************************************************
     The ICloudTable generic interface represents a CRUD interface
     into a table and is defined in Abstractions\ICloudTable.cs
    ****************************************************************/

    public interface ICloudTable<T> where T : TableData
    {
        Task<T> CreateItemAsync(T item);
        Task<T> ReadItemAsync(string id);
        Task<T> UpdateItemAsync(T item);
        Task DeleteItemAsync(T item);
        Task<ICollection<T>> ReadAllItemsAsync();
        Task<ICollection<T>> ReadItemsAsync(int start, int count);
    }
}
