using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RESTAURANT.API.DAL
{
    public class Detail : RestaurantBase
    {
        [Column(Order = 5)]
        public bool? IsTakeAway { get; set; }
        [Column(Order = 6)]
        public int? Count { get; set; }
        [ForeignKey("Food")]
        public int? FoodId { get; set; }
        public virtual Food Food { get; set; }

        [ForeignKey("Kind")]
        public int? KindId { get; set; }
        public virtual Kind Kind { get; set; }

        [ForeignKey("Drink")]
        public int? DrinkId { get; set; }
        public virtual Drink Drink { get; set; }

        public int OrderId { get; set; }
        public decimal? Price { get; set; }
        //public virtual Order Order { get; set; }
        //public string JsonExcept { get; set; }
        //public string JsonUtility { get; set; }
    }
}
