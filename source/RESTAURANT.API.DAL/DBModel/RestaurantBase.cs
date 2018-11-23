using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RESTAURANT.API.DAL
{
    public class RestaurantBase
    {
        public int ID { get; set; }
        [StringLength(255)]
        public string Title { get; set; }
        public string Note { get; set; }
        public string UserCreate { get; set; }
        public System.Nullable<DateTime> CreateDate { get; set; }
        public string UserUpdate { get; set; }
        public System.Nullable<DateTime> UpdateDate { get; set; }
    }
}