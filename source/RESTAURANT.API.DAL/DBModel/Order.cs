using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace RESTAURANT.API.DAL
{
    public class Order : RestaurantBase
    {
        [Column(Order = 5)]
        public bool? IsTakeAway { get; set; }
        [ForeignKey("Table")]
        public int TableId { get; set; }
        public virtual Table Table { get; set; }
        [ForeignKey("Status")]
        public int StatusId { get; set; }
        public virtual Status Status { get; set; }
        //public ICollection<Detail> Details { get; set; }
        [ForeignKey("OrderId")]
        public virtual List<Detail> Details { get; set; }
        public decimal Amount { get; set; }
    }
}
