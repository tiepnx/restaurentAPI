

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RESTAURANT.API.DAL
{
    public class FoodGroup: RestaurantBase
    {
        [ForeignKey("FoodGroupId")]
        public virtual List<Food> Foods { get; set; }
    }
}