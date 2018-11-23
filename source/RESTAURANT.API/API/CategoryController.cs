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
        [Route("GetList")]

        public IHttpActionResult GetList()
        {
            List<Category> listView = null;

            using (CategoryService sv = new CategoryService())
            {
                listView = sv.GetList();
            }
            return Ok(new { listView });
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
    }
}