namespace RESTAURANT.API.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;

    public partial class RestaurentCtx : DbContext
    {
        public RestaurentCtx()
            : base("name=T5Context")
        {
            //Database.SetInitializer(new CreateDatabaseIfNotExists<RestaurentCtx>());
            Database.SetInitializer(new RestaurentCtxInitializer());
        }


        public DbSet<Status> Status { get; set; }
        public DbSet<Food> Food { get; set; }
        public DbSet<FoodGroup> FoodGroup { get; set; }
        public DbSet<Drink> Drink { get; set; }
        public DbSet<DrinkGroup> DrinkGroup { get; set; }
        public virtual DbSet<Except> Except { get; set; }
        public DbSet<Kind> Kind { get; set; }
        public virtual DbSet<Utility> Utility { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Detail> Details { get; set; }
        public DbSet<Table> Table { get; set; }
        public DbSet<OFS> OFS { get; set; }
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
            
            //modelBuilder.Entity<Detail>()
            //     .HasOptional<Kind>(d => d.Kind)
            //     .WithMany().WillCascadeOnDelete(false);
            base.OnModelCreating(modelBuilder);
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
            if (entityEntry.State == EntityState.Added)
            {
                //trackUpdateClass.rowguid = Guid.NewGuid();
                trackUpdateClass.Created = DateTime.UtcNow;
            }
            else
            {
                trackUpdateClass.Modified = DateTime.UtcNow;
            }
        }
    }
}
