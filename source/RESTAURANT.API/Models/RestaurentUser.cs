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
        [StringLength(255)]
        public string FullName { get; set; }
        [StringLength(255)]
        public string FirstName { get; set; }
        [StringLength(255)]
        public string LastName { get; set; }
        [StringLength(1024)]
        public string Address { get; set; }
        [StringLength(255)]
        public string Avatar { get; set; }
        [StringLength(2048)]
        public string Notes { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool? IsActive { get; set; }
        public bool? AllowInPast { get; set; }
        public DateTime? AllowedDate { get; set; }

        public Guid? ResetPasswordKey { get; set; }
        public DateTime? ExpiredResetPassword { get; set; }
        [StringLength(125)]
        public string Provider { get; set; }
        public DateTime? Created { get; set; }
        [StringLength(255)]
        public string CreatedBy { get; set; }
        public DateTime? Modified { get; set; }
        [StringLength(255)]
        public string ModifiedBy { get; set; }
        [StringLength(128)]
        public string ClientID { get; set; }
        
        public System.Nullable<Guid> OFSKey { get; set; }
    }
}