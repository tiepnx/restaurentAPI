using Microsoft.AspNet.Identity.EntityFramework;
using RESTAURANT.API.Entities;
using RESTAURANT.API.Models;
using System;
using System.Data.Entity;

namespace RESTAURANT.API.AppCode
{
    //public class AuthContext : IdentityDbContext<IdentityUser>
    public class AuthContext : IdentityDbContext<RestaurentUser>
    {

        public AuthContext() : base("T5Context")
        {

        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}