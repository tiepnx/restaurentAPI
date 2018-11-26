using System.ComponentModel.DataAnnotations.Schema;

namespace RESTAURANT.API.DAL
{
    public class Detail : RestaurantBase
    {
        public int Count { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        [ForeignKey("Kind")]
        public int KindId { get; set; }
        public virtual Kind Kind { get; set; }
        public string JsonExcept { get; set; }
        public string JsonUtility { get; set; }
    }
}
