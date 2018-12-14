using RESTAURANT.API.App_Start;
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
    [RoutePrefix("api/order")]
    public class OrderController: ApiController
    {
        [HttpGet]
        [Authorize]
        [Route("items")]        
        public IHttpActionResult Gets()
        {
            List<Order> items = null;

            using (OrderService svc = new OrderService())
            {
                var ofs = Common.GetOFSKey(Request.GetRequestContext().Principal as ClaimsPrincipal);
                items = svc.GetList(ofs);
            }
            return Ok(new { items });
        }
        [HttpGet]
        [Route("items/{rowId}")]

        public IHttpActionResult Get(Guid rowId)
        {
            Order item = null;

            using (OrderService svc = new OrderService())
            {
                item = svc.Get(rowId);
            }
            return Ok(new { item });
        }
        [Authorize]
        [HttpPost]
        public IHttpActionResult Post(Order item)
        {
            var ofs = Common.GetOFSKey(Request.GetRequestContext().Principal as ClaimsPrincipal);
            item.OfsKey = ofs;
            using (OrderService svc = new OrderService())
            {
                item = svc.Insert(item, HttpContext.Current.User.Identity.Name);
            }
            NotificationHub.AddOrder(ofs.ToString(), item.CreatedBy, item.RowGuid.Value.ToString());
            return Ok(new {
                item.RowGuid, item.CreatedBy,item.Title
            });
        }
        [Authorize]
        [HttpDelete]
        [Route("{rowId}")]
        public IHttpActionResult Delete(Guid rowId)
        {
            bool result = false;
            using (OrderService svc = new OrderService())
            {
                var ofs = Common.GetOFSKey(Request.GetRequestContext().Principal as ClaimsPrincipal);
                result = svc.Delete(ofs, rowId, HttpContext.Current.User.Identity.Name);
            }
            return Ok(new { result });
        }
    }
}