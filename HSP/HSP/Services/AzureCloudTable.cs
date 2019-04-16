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

        /// <summary>
        /// Keep reading records until there are no more records to read.
        /// This code will always make a minimum of 2 requests if there is any data.
        /// If you have 75 records, three requests will be made - the first will bring down 50 records, 
        /// the second 25 records and the third no records.
        /// </summary>
        /// <returns>AllItems</returns>
        public async Task<ICollection<T>> ReadAllItemsAsync()
        {
            List<T> allItems = new List<T>();

            var pageSize = 50;
            var hasMore = true;
            while (hasMore)
            {
                var pageOfItems = await table.Skip(allItems.Count).Take(pageSize).ToListAsync();
                if (pageOfItems.Count > 0)
                {
                    allItems.AddRange(pageOfItems);
                }
                else
                {
                    hasMore = false;
                }
            }
            return allItems;
        }

        public Task<T> ReadItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// UI thread will pause as the data is loaded, but the resulting UI will be less memory hungry and overall more responsive.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<ICollection<T>> ReadItemsAsync(int start, int count)
        {
            return await table.Skip(start).Take(count).ToListAsync();
        }

        public async Task<T> UpdateItemAsync(T item)
        {
            await table.UpdateAsync(item);
            return item;
        }
        #endregion

    }
}
