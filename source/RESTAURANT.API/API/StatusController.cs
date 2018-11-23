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
        [Route("GetList")]

        public IHttpActionResult GetList()
        {
            List<Status> listView = null;

            using (StatusService svc = new StatusService())
            {
                listView = svc.GetList();
            }
            return Ok(new { listView });
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
    }
}