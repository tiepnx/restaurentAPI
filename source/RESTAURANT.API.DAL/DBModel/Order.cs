using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RESTAURANT.API.DAL
{
    public class Order : RestaurantBase
    {
        public Table Table { get; set; }        
        public Status Status { get; set; }
        //public ICollection<Detail> Details { get; set; }

    }
}
