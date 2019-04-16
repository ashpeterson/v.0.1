using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using Backend.DataObjects;
using Backend.Models;

namespace Backend.Controllers
{
    public class SyncController : TableController<Sync>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<Sync>(context, Request, enableSoftDelete: true);
        }

        // GET tables/Sync
        public IQueryable<Sync> GetAllSync()
        {
            return Query(); 
        }

        // GET tables/Sync/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Sync> GetSync(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Sync/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Sync> PatchSync(string id, Delta<Sync> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Sync
        public async Task<IHttpActionResult> PostSync(Sync item)
        {
            Sync current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Sync/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteSync(string id)
        {
             return DeleteAsync(id);
        }

      

    }
}
