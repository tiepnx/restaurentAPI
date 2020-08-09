
using System;
using System.ComponentModel.DataAnnotations;

namespace RESTAURANT.API.DAL
{
    public class OFS: RestaurantBase
    {
        //public OFS(Guid rowId)
        //{
        //    this.RowGuid = rowId;
        //}
        [StringLength(2048)]
        public string Address { get; set; }
        [StringLength(255)]
        public string Phone { get; set; }
    }
}
