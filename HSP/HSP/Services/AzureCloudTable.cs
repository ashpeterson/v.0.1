using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using HSP.Abstractions;
using System.Text;

namespace HSP.Services
{
        //note here that the Azure Mobile Apps Client SDK does a lot of 
        //the work for us.In fact, we are just wrapping the basic 
        //interface here. This won't normally be the case, but you 
        //can see that the majority of the code for dealing with the 
        //remote server is done for us.

    public class AzureCloudTable<T> : ICloudTable<T> where T : TableData
    {
        MobileServiceClient client;
        IMobileServiceTable<T> table;

        public AzureCloudTable(MobileServiceClient client)
        {
            this.client = client;
            this.table = client.GetTable<T>();
        }

        #region ICloudTable Implementation 
        public async Task<T> CreateItemAsync(T item)
        {
            await table.InsertAsync(item);
            return item;
        }

        public async Task DeleteItemAsync (T item)
        {
            await table.DeleteAsync(item);
        }

        public async Task <ICollection<T>> ReadAllItemsAsync()
        {
            return await table.ToListAsync();
        }

        public async Task<T> ReadItemAsync(string id)
        {
            return await table.LookupAsync(id);
        }

        public async Task<T> UpdateItemAsync(T item)
        {
            await table.UpdateAsync(item);
            return item;
        }
        #endregion

    }
}
