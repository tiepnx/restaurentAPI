namespace RESTAURANT.API.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;

    public partial class RestaurentModel : DbContext
    {
        public RestaurentModel()
            : base("name=T5Context")
        {
        }


        public DbSet<Status> Status { get; set; }
        public DbSet<Category> Category { get; set; }
        public virtual DbSet<Except> Except { get; set; }
        public DbSet<Kind> Kind { get; set; }
        public virtual DbSet<Utility> Utility { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Detail> Details { get; set; }
        public DbSet<Table> Table { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Detail>()
            //    .HasRequired<Category>(dt => dt.CurrentCategory)
            //    .WithMany(c => c.Details)
            //    .HasForeignKey<int>(dt => dt.CurrentCategoryId);
            //modelBuilder.Entity<Order>()
            //    .HasRequired<Status>(o => o.Status)
            //    .WithMany(st => st.Orders)
            //    .HasForeignKey<int>(o => o.StatusId);

            
        }

        public override int SaveChanges()
        {
            HandleChangeTracking();
            return base.SaveChanges();
        }
        private void HandleChangeTracking()
        {
            foreach (var entity in ChangeTracker.Entries()
               .Where(e => e.State == EntityState.Added
                   || e.State == EntityState.Modified))
            {
                UpdateTrackedEntity(entity);
            }
        }
        /// <summary>
        /// Looks at everything that has changed and
        /// applies any further action if required.
        /// </summary>
        /// <param id="entityEntry""></param>
        /// <returns></returns>
        private static void UpdateTrackedEntity(DbEntityEntry entityEntry)
        {
            var trackUpdateClass = entityEntry.Entity as IModifiedEntity;
            if (trackUpdateClass == null) return;
            trackUpdateClass.Modified = DateTime.UtcNow;
            if (entityEntry.State == EntityState.Added)
            {
                //trackUpdateClass.rowguid = Guid.NewGuid();
                trackUpdateClass.Created = DateTime.UtcNow;
            }
        }
    }
}
