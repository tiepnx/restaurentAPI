using RESTAURANT.API.DAL;
using RESTAURANT.API.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace RESTAURANT.API.API
{
    [RoutePrefix("api/detail")]
    public class DetailController: ApiController
    {
        [HttpGet]
        [Route("GetList")]

        public IHttpActionResult GetList()
        {
            List<Detail> items = null;

            using (DetailService svc = new DetailService())
            {
                items = svc.GetList();
            }
            return Ok(new { items });
        }
        [Authorize]
        [HttpPost]
        public IHttpActionResult Post(Detail item)
        {
            int? id = null;
            
            using (DetailService svc = new DetailService())
            {
                id = svc.Insert(item, HttpContext.Current.User.Identity.Name);
            }
            return Ok(new { id });
        }
    }
}