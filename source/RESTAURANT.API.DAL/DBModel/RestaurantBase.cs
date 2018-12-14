using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RESTAURANT.API.DAL
{
    public class RestaurantBase: IModifiedEntity
    {
        [Column(Order = 0)]
        public int ID { get; set; }
        [Column(Order = 1)]
        [StringLength(255)]
        public string Title { get; set; }
        [StringLength(2048)]
        public string Note { get; set; }
        //public string CreatedBy { get; set; }
        //public System.Nullable<DateTime> Created { get; set; }
        //public string ModifiedBy { get; set; }
        //public System.Nullable<DateTime> Modified { get; set; }
        //public System.Nullable<Boolean> Deleted { get; set; }
        public virtual System.Nullable<DateTime> Modified { get; set; }
        public virtual System.Nullable<DateTime> Created { get; set; }
        [Column(Order = 3)]
        public virtual System.Nullable<Guid> RowGuid { get; set; }
        [Column(Order = 4)]
        public virtual System.Nullable<Guid> OfsKey { get; set; }
        
        public virtual string CreatedBy { get; set; }
        public virtual string ModifiedBy { get; set; }
        [Column(Order = 2)]
        public virtual System.Nullable<Boolean> Deleted { get; set; }
    }
}