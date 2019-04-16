using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using Backend.DataObjects;
using Backend.Models;
using System.Security.Principal;
using Microsoft.Azure.Mobile.Server.Authentication;
using System.Collections.Generic;
using Backend.Extensions;
using System.Net;

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
        public async Task<IQueryable<Sync>> GetAllSyncAsync()
        {
            var groups = await GetGroups();
            return Query().PerGroupFilter(groups);
        }

        // GET tables/Sync/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public async Task<SingleResult<Sync>> GetSyncAsync(string id)
        {
            var groups = await GetGroups();
            return new SingleResult<Sync>(Lookup(id).Queryable.PerGroupFilter(groups));
        }

        /// <summary>
        /// Validator to determine if the provided group is in the list of groups
        /// </summary>
        /// <param name="group">The group name</param>
        public async Task ValidateGroup(string group)
        {
            var groups = await GetGroups();
            if (!groups.Contains(group))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        // PATCH tables/Sync/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public async Task<Sync> PatchSync(string id, Delta<Sync> patch)
        {
            await ValidateGroup(patch.GetEntity().GroupId);
            return await UpdateAsync(id, patch);
        }

        // POST tables/Sync
        public async Task<IHttpActionResult> PostSync(Sync item)
        {
            await ValidateGroup(item.GroupId);
            Sync current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Sync/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteSync(string id)
        {
             return DeleteAsync(id);
        }
        
        /// <summary>
        /// Get the list of groups from the claims
        /// </summary>
        /// <returns>The list of groups</returns>
        public async Task<List<string>> GetGroups()
        {
            var creds = await User.GetAppServiceIdentityAsync<AzureActiveDirectoryCredentials>(Request);
            return creds.UserClaims
                .Where(claim => claim.Type.Equals("groups"))
                .Select(claim => claim.Value)
                .ToList();
        }
    }
}
