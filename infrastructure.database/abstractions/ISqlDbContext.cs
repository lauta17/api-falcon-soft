using infrastructure.database.model;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.database.abstractions
{
    public interface ISqlDbContext
    {
        DbSet<OrderDb> Orders { get; set; }
        DbSet<ProductDb> Products { get; set; }
        DbSet<UserDb> Users { get; set; }
        int SaveChanges();
        Task SaveChangesAsync();
    }
}
