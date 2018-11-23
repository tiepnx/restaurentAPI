namespace RESTAURANT.API.DAL
{
    using System.Data.Entity;

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
    }
}
