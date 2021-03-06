﻿using RESTAURANT.API.DAL;
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
    [RoutePrefix("api/utility")]
    public class UtilityController: ApiController
    {
        [HttpGet]
        [Route("items")]
        [Authorize]
        public IHttpActionResult GetList()
        {
            List<Utility> items = null;

            using (UtilityService svc = new UtilityService())
            {
                var ofs = Common.GetOFSKey(Request.GetRequestContext().Principal as ClaimsPrincipal);
                items = svc.GetList(ofs);
            }
            return Ok(new { items });
        }
        [Authorize]
        [HttpPost]
        public IHttpActionResult Post(Utility item)
        {
            int? id = null;
            var ofs = Common.GetOFSKey(Request.GetRequestContext().Principal as ClaimsPrincipal);
            item.OfsKey = ofs;
            using (UtilityService svc = new UtilityService())
            {
                id = svc.Insert(item, HttpContext.Current.User.Identity.Name);
            }
            return Ok(new { id });
        }
        
        [Authorize]
        [HttpPut]
        public IHttpActionResult Put(Utility item)
        {
            using (UtilityService svc = new UtilityService())
            {
                svc.Update(item, HttpContext.Current.User.Identity.Name);
            }
            return Ok(item);

        }
        [Authorize]
        [HttpDelete]
        public IHttpActionResult Delete(Utility item)
        {
            using (UtilityService svc = new UtilityService())
            {
                svc.Delete(item, HttpContext.Current.User.Identity.Name);
            }
            return Ok(item);
        }
    }
}