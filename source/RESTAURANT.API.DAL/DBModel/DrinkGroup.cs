using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RESTAURANT.API.DAL
{
    public class DrinkGroup : RestaurantBase
    {
        [ForeignKey("DrinkGroupId")]
        public virtual List<Drink> Drinks { get; set; }
    }
}