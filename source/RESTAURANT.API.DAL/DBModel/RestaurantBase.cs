using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RESTAURANT.API.DAL
{
    public class RestaurantBase: IModifiedEntity
    {
        public int ID { get; set; }
        [StringLength(255)]
        public string Title { get; set; }
        public string Note { get; set; }
        //public string CreatedBy { get; set; }
        //public System.Nullable<DateTime> Created { get; set; }
        //public string ModifiedBy { get; set; }
        //public System.Nullable<DateTime> Modified { get; set; }
        //public System.Nullable<Boolean> Deleted { get; set; }
        public virtual System.Nullable<DateTime> Modified { get; set; }
        public virtual System.Nullable<DateTime> Created { get; set; }
        //Guid rowguid { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual string ModifiedBy { get; set; }
        public virtual System.Nullable<Boolean> Deleted { get; set; }
    }
}