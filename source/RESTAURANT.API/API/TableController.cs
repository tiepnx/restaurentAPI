using RESTAURANT.API.DAL;
using RESTAURANT.API.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace RESTAURANT.API.API
{
    [RoutePrefix("api/table")]
    public class TableController : ApiController
    {
        [HttpGet]
        [Route("items")]

        public IHttpActionResult GetList()
        {
            List<Table> listView = null;

            using (TableService tsv = new TableService())
            {
                listView = tsv.GetList();
            }
            return Ok(new { listView });
        }
        [Authorize]
        [HttpPost]
        public IHttpActionResult Post(Table item)
        {
            int? id = null;
            
            using (TableService tsv = new TableService())
            {
                id = tsv.Insert(item, HttpContext.Current.User.Identity.Name);
            }
            return Ok(new { id });
        }
        [Authorize]
        [HttpPut]
        public IHttpActionResult Put(Table item)
        {
            using (TableService svc = new TableService())
            {
                svc.Update(item, HttpContext.Current.User.Identity.Name);
            }
            return Ok(item);

        }
        [Authorize]
        [HttpDelete]
        public IHttpActionResult Delete(Table item)
        {
            using (TableService svc = new TableService())
            {
                svc.Delete(item, HttpContext.Current.User.Identity.Name);
            }
            return Ok(item);
        }
    }
}