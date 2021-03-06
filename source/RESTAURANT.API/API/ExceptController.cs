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
    [RoutePrefix("api/except")]
    public class ExceptController: ApiController
    {
        [HttpGet]
        [Route("items")]
        [Authorize]
        public IHttpActionResult GetList()
        {
            List<Except> items = null;

            using (ExceptService svc = new ExceptService())
            {
                var ofs = Common.GetOFSKey(Request.GetRequestContext().Principal as ClaimsPrincipal);
                items = svc.GetList(ofs);
            }
            return Ok(new { items });
        }
        [Authorize]
        [HttpPost]
        public IHttpActionResult Post(Except item)
        {
            int? id = null;
            var ofs = Common.GetOFSKey(Request.GetRequestContext().Principal as ClaimsPrincipal);
            item.OfsKey = ofs;
            using (ExceptService svc = new ExceptService())
            {
                id = svc.Insert(item, HttpContext.Current.User.Identity.Name);
            }
            return Ok(new { id });
        }
        [Authorize]
        [HttpPut]
        public IHttpActionResult Put(Except item)
        {
            using (ExceptService svc = new ExceptService())
            {
                svc.Update(item, HttpContext.Current.User.Identity.Name);
            }
            return Ok(item);

        }
        [Authorize]
        [HttpDelete]
        public IHttpActionResult Delete(Except item)
        {
            using (ExceptService svc = new ExceptService())
            {
                svc.Delete(item, HttpContext.Current.User.Identity.Name);
            }
            return Ok(item);
        }
    }
}