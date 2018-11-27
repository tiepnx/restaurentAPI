using RESTAURANT.API.DAL;
using RESTAURANT.API.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace RESTAURANT.API.API
{
    [RoutePrefix("api/status")]
    public class StatusController: ApiController
    {
        [HttpGet]
        [Route("items")]

        public IHttpActionResult GetList()
        {
            List<Status> items = null;

            using (StatusService svc = new StatusService())
            {
                items = svc.GetList();
            }
            return Ok(new { items });
        }
        [Authorize]
        [HttpPost]
        public IHttpActionResult Post(Status item)
        {
            int? id = null;
            
            using (StatusService svc = new StatusService())
            {
                id = svc.Insert(item, HttpContext.Current.User.Identity.Name);
            }
            return Ok(new { id });
        }
        [Authorize]
        [HttpPut]
        public IHttpActionResult Put(Status item)
        {
            using (StatusService svc = new StatusService())
            {
                svc.Update(item, HttpContext.Current.User.Identity.Name);
            }
            return Ok(item);

        }
        [Authorize]
        [HttpDelete]
        public IHttpActionResult Delete(Status item)
        {
            using (StatusService svc = new StatusService())
            {
                svc.Delete(item, HttpContext.Current.User.Identity.Name);
            }
            return Ok(item);
        }
        
    }
}