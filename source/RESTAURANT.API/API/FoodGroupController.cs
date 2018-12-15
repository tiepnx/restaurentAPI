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
    [RoutePrefix("api/foodgroup")]
    public class FoodGroupController : ApiController
    {
        [HttpGet]
        [Route("items")]
        [Authorize]
        public IHttpActionResult GetList()
        {
            List<FoodGroup> items = null;

            using (FoodGroupService sv = new FoodGroupService())
            {
                var ofs = Common.GetOFSKey(Request.GetRequestContext().Principal as ClaimsPrincipal);
                items = sv.GetList(ofs);
            }
            return Ok(new { items });
        }
        [Authorize]
        [HttpPost]
        public IHttpActionResult Post(FoodGroup item)
        {
            int? id = null;
            var ofs = Common.GetOFSKey(Request.GetRequestContext().Principal as ClaimsPrincipal);
            item.OfsKey = ofs;
            using (FoodGroupService sv = new FoodGroupService())
            {
                id = sv.Insert(item, HttpContext.Current.User.Identity.Name);
            }
            return Ok(new { id });
        }
        [Authorize]
        [HttpPut]
        public IHttpActionResult Put(FoodGroup item)
        {
            using (FoodGroupService svc = new FoodGroupService())
            {
                svc.Update(item, HttpContext.Current.User.Identity.Name);
            }
            return Ok(item);

        }
        [Authorize]
        [HttpDelete]
        public IHttpActionResult Delete(FoodGroup item)
        {
            using (FoodGroupService svc = new FoodGroupService())
            {
                svc.Delete(item, HttpContext.Current.User.Identity.Name);
            }
            return Ok(item);
        }
    }
}