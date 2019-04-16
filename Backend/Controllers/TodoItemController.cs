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
using System.Net;
using System.Collections.Generic;
using System.Security.Principal;
using Microsoft.Azure.Mobile.Server.Authentication;

namespace Backend.Controllers
{
    /*-------------------------------------------------------------------------------------------------
     The TableController is the central processing for the database access layer. 
     It handles all the OData capabilities for us and exposes these as REST endpoints within our WebAPI. 

     OData is a specification for accessing table data on the Internet. It provides a mechanism for 
     querying and manipulating data within a table. Entity Framework is a common data access layer for 
     ASP.NET applications.
    ------------------------------------------------------------------------------------------------- */

    public class TodoItemController : TableController<TodoItem>
    {
        [EnableQuery(PageSize = 10)]
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<TodoItem>(context, Request);
        }

        // GET tables/TodoItem
        public IQueryable<TodoItem> GetAllTodoItems()
        {
            return Query().PerUserFilter(UserId);
            //return Query().Where(item => item.UserId.Equals(UserId));
            //return Query();
        }

        // GET tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<TodoItem> GetTodoItem(string id)
        {
            return new SingleResult<TodoItem>(Lookup(id).Queryable.PerUserFilter(UserId));
            //return Lookup(id);
        }

        // PATCH tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<TodoItem> PatchTodoItem(string id, Delta<TodoItem> patch)
        {
            ValidateOwner(id);
            return UpdateAsync(id, patch);
        }
         
        // POST tables/TodoItem
        public async Task<IHttpActionResult> PostTodoItem(TodoItem item)
        {
            item.UserId = UserId;
            TodoItem current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteTodoItem(string id)
        {
            ValidateOwner(id);
            return DeleteAsync(id);
        }
        public string UserId
        {
            get
            {
                var principal = this.User as ClaimsPrincipal;
                return principal.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
        }

        /// <summary>
        /// Validate that the UserId owns the id we are updating.
        /// </summary>
        /// <param name="id"></param>
        private void ValidateOwner(string id)
        {
            var result = Lookup(id).Queryable.PerUserFilter(UserId).FirstOrDefault<TodoItem>();
            if (result == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

       
    }
}