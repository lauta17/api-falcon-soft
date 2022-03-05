using infrastructure.database.abstractions;
using infrastructure.database.configurations;
using infrastructure.database.model;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.database
{
    public class SqlDbContext : DbContext, ISqlDbContext
    {
        public DbSet<OrderDb> Orders { get; set; }
        public DbSet<ProductDb> Products { get; set; }
        public DbSet<UserDb> Users { get; set; }

        public SqlDbContext(DbContextOptions<SqlDbContext> options)
            : base(options)
        {
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public Task SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            new OrderConfiguration(modelBuilder.Entity<OrderDb>());
            new ProductConfiguration(modelBuilder.Entity<ProductDb>());
            new UserConfiguration(modelBuilder.Entity<UserDb>());
        }
    }
}
