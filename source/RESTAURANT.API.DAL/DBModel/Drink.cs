using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RESTAURANT.API.DAL
{
    public class Drink : RestaurantBase
    {
        public int DrinkGroupId { get; set; }
        public decimal Price { get; set; }
    }
}
