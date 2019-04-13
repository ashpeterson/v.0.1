using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Tables;
using Backend.DataObjects;

namespace Backend.Models
{
    public class MobileServiceContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to alter your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
        // To enable Entity Framework migrations in the cloud, please ensure that the 
        // service name, set by the 'MS_MobileServiceName' AppSettings in the local 
        // Web.config, is the same as the service name when hosted in Azure.

        /*---------------------------------------------------------------------------------------------------------------------------------------------------
        Models\MobileServiceContext.cs. Azure Mobile Apps is heavily dependent on Entity Framework v6.x and the DbContext is a central part of that library.
        ---------------------------------------------------------------------------------------------------------------------------------------------------*/


        private const string connectionStringName = "Name=MS_TableConnectionString";

        public MobileServiceContext() : base(connectionStringName)
        {
            Database.Log = s => WriteLog(s);
        }

        public void WriteLog(string msg)
        {
            System.Diagnostics.Debug.WriteLine(msg);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(
                new AttributeToColumnAnnotationConvention<TableColumnAttribute, string>(
                    "ServiceTableColumn", (property, attributes) => attributes.Single().ColumnType.ToString()));
        }

        public DbSet<DataObjects.TodoItem> TodoItems { get; set; }
        public DbSet<DataObjects.Initial> Initial { get; set; }

        public System.Data.Entity.DbSet<Backend.DataObjects.Sync> Syncs { get; set; }
    }
}
