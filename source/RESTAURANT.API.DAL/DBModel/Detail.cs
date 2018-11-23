using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTAURANT.API.DAL
{
    public class Detail : RestaurantBase
    {
        public Category Category { get; set; }
        public int Count { get; set; }        
        public Kind Kind { get; set; }
        public string Except { get; set; }
        public string Utility { get; set; }
    }
}
