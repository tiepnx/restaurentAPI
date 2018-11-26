using RESTAURANT.API.DAL;
using RESTAURANT.API.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace RESTAURANT.API.API
{
    [RoutePrefix("api/kind")]
    public class KindController : ApiController
    {
        [HttpGet]
        [Route("items")]

        public IHttpActionResult GetList()
        {
            List<Kind> listView = null;

            using (KindService tsv = new KindService())
            {
                listView = tsv.GetList();
            }
            return Ok(new { listView });
        }
        [Authorize]
        [HttpPost]
        public IHttpActionResult Post(Kind item)
        {
            int? id = null;
            
            using (KindService tsv = new KindService())
            {
                id = tsv.Insert(item, HttpContext.Current.User.Identity.Name);
            }
            return Ok(new { id });
        }
        [Authorize]
        [HttpPut]
        public IHttpActionResult Put(Kind item)
        {
            using (KindService svc = new KindService())
            {
                svc.Update(item, HttpContext.Current.User.Identity.Name);
            }
            return Ok(item);

        }
        [Authorize]
        [HttpDelete]
        public IHttpActionResult Delete(Kind item)
        {
            using (KindService svc = new KindService())
            {
                svc.Delete(item, HttpContext.Current.User.Identity.Name);
            }
            return Ok(item);
        }
    }
}