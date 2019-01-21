using System;
using System.Collections.Generic;
using System.Text;

namespace HSP.Abstractions
{
    /**************************************************************
     The ICloudTable generic interface represents a CRUD interface
     into a table and is defined in Abstractions\ICloudTable.cs
    ****************************************************************/

    interface ICloudTable<T> where T : TableData
    {
        Tasks<T> CreateItemAsync(T item);
        Task<T> ReadItemAsync(string id);
        Task<T> UpdateItemAsync(T item);
        Task DeleteItemAsync(T item);

        Task<ICollection<T>> ReadAllItemsAsync();
    }
}
