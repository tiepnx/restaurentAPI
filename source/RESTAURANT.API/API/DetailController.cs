using RESTAURANT.API.DAL;
using RESTAURANT.API.DAL.Services;
using RESTAURANT.API.helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
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
        [Authorize]
        [HttpDelete]
        [Route("{rowId}")]
        public IHttpActionResult Delete(Guid rowId)
        {
            bool flag = false;

            using (DetailService svc = new DetailService())
            {
                var ofs = Common.GetOFSKey(Request.GetRequestContext().Principal as ClaimsPrincipal);
                flag = svc.Delete(ofs, rowId, HttpContext.Current.User.Identity.Name);
            }
            return Ok(new { flag });
        }
    }
}