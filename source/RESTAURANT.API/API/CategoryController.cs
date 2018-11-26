using RESTAURANT.API.DAL;
using RESTAURANT.API.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace RESTAURANT.API.API
{
    [RoutePrefix("api/category")]
    public class CategoryController : ApiController
    {
        [HttpGet]
        [Route("items")]

        public IHttpActionResult GetList()
        {
            List<Category> items = null;

            using (CategoryService sv = new CategoryService())
            {
                items = sv.GetList();
            }
            return Ok(new { items });
        }
        [Authorize]
        [HttpPost]
        public IHttpActionResult Post(Category item)
        {
            int? id = null;

            using (CategoryService sv = new CategoryService())
            {
                id = sv.Insert(item, HttpContext.Current.User.Identity.Name);
            }
            return Ok(new { id });
        }
        [Authorize]
        [HttpPut]
        public IHttpActionResult Put(Category item)
        {
            using (CategoryService svc = new CategoryService())
            {
                svc.Update(item, HttpContext.Current.User.Identity.Name);
            }
            return Ok(item);

        }
        [Authorize]
        [HttpDelete]
        public IHttpActionResult Delete(Category item)
        {
            using (CategoryService svc = new CategoryService())
            {
                svc.Delete(item, HttpContext.Current.User.Identity.Name);
            }
            return Ok(item);
        }
    }
}