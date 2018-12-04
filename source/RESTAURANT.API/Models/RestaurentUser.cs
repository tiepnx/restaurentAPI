using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RESTAURANT.API.Models
{
    public class RestaurentUser: IdentityUser
    {
        [StringLength(250)]
        public string FirstName { get; set; }

        [StringLength(250)]
        public string LastName { get; set; }

        public bool IsActive { get; set; }
        [StringLength(128)]
        public string ClientID { get; set; }
        public System.Nullable<DateTime> CreatedDate { get; set; }
        public System.Nullable<DateTime> UpdateDate { get; set; }
        [StringLength(250)]
        public string UserUpdate { get; set; }
        public System.Nullable<Guid> OFSKey { get; set; }
    }
}