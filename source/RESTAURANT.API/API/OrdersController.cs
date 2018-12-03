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
        [Route("items")]

        public IHttpActionResult Gets()
        {
            List<Order> items = null;

            using (OrderService svc = new OrderService())
            {
                items = svc.GetList();
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
            Guid? rowGuid = null;
            
            using (OrderService svc = new OrderService())
            {
                rowGuid = svc.Insert(item, HttpContext.Current.User.Identity.Name);
            }
            return Ok(new { rowGuid });
        }
    }
}