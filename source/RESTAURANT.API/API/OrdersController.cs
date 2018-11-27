using RESTAURANT.API.DAL;
using RESTAURANT.API.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace RESTAURANT.API.API
{
    [RoutePrefix("api/order")]
    public class OrderController: ApiController
    {
        [HttpGet]
        [Route("GetList")]

        public IHttpActionResult GetList()
        {
            List<Order> items = null;

            using (OrderService svc = new OrderService())
            {
                items = svc.GetList();
            }
            return Ok(new { items });
        }
        [Authorize]
        [HttpPost]
        public IHttpActionResult Post(Order item)
        {
            int? id = null;
            
            using (OrderService svc = new OrderService())
            {
                id = svc.Insert(item, HttpContext.Current.User.Identity.Name);
            }
            return Ok(new { id });
        }
    }
}