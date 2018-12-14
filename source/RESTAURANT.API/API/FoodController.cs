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
    [RoutePrefix("api/food")]
    public class FoodController : ApiController
    {
        [HttpGet]
        [Route("items")]
        [Authorize]
        public IHttpActionResult GetList()
        {
            List<Food> items = null;

            using (FoodService sv = new FoodService())
            {
                var ofs = Common.GetOFSKey(Request.GetRequestContext().Principal as ClaimsPrincipal);
                items = sv.GetList(ofs);
            }
            return Ok(new { items });
        }
        [Authorize]
        [HttpPost]
        public IHttpActionResult Post(Food item)
        {
            int? id = null;
            var ofs = Common.GetOFSKey(Request.GetRequestContext().Principal as ClaimsPrincipal);
            item.OfsKey = ofs;
            using (FoodService sv = new FoodService())
            {
                id = sv.Insert(item, HttpContext.Current.User.Identity.Name);
            }
            return Ok(new { id });
        }
        [Authorize]
        [HttpPut]
        public IHttpActionResult Put(Food item)
        {
            using (FoodService svc = new FoodService())
            {
                svc.Update(item, HttpContext.Current.User.Identity.Name);
            }
            return Ok(item);

        }
        [Authorize]
        [HttpDelete]
        public IHttpActionResult Delete(Food item)
        {
            using (FoodService svc = new FoodService())
            {
                svc.Delete(item, HttpContext.Current.User.Identity.Name);
            }
            return Ok(item);
        }
    }
}