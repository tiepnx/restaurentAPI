using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace RESTAURANT.API.DAL
{
    public class Order : RestaurantBase
    {
        [ForeignKey("Table")]
        public int TableId { get; set; }
        public virtual Table Table { get; set; }
        [ForeignKey("Status")]
        public int StatusId { get; set; }
        public virtual Status Status { get; set; }
        //public ICollection<Detail> Details { get; set; }
        public virtual List<Detail> Details { get; set; }
    }
}
