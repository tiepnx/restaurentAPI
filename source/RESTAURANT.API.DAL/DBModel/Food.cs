

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RESTAURANT.API.DAL
{
    public class Food: RestaurantBase
    {
        public int? FoodGroupId { get; set; }
        public decimal Price { get; set; }
    }
}