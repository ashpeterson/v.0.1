using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using Backend.DataObjects;
using Backend.Models;
using System.Security.Claims;
using Backend.Extensions;
using System.Web;
using System.Data.Entity;

namespace Backend.Controllers
{
    public class MessagesController : TableController<Messages>
    {
   
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<Messages>(context, Request);
        }

        public string UserId => ((ClaimsPrincipal)User).FindFirst(ClaimTypes.NameIdentifier).Value;
     
        ////GET tables/Messages
        //public IQueryable<Messages> GetAllMessage()
        //{
        //    //TODO - FIX  THESE METHODS
        //   // return Query().OwnedByFriends(context.Friends, UserId);

        //}

        //// GET tables/Message/48D68C86-6EA6-4C25-AA33-223FC9A27959
        //public SingleResult<Messages> GetMessage(string id)
        //{
        //    //TODO - FIX  THESE METHODS
        //   //return new SingleResult<Messages>(Lookup(id).Queryable.OwnedByFriends(context.Friends, UserId));
        //}

        // PATCH tables/Messages/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Messages> PatchMessages(string id, Delta<Messages> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Message
        public async Task<IHttpActionResult> PostMessage(Messages item)
        {
            item.UserId = UserId;
            Messages current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Messages/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteMessages(string id)
        {
             return DeleteAsync(id);
        }
    }
}
