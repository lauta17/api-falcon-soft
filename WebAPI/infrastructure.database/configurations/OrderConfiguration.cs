using infrastructure.database.model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace infrastructure.database.configurations
{
    public class OrderConfiguration
    {
        public OrderConfiguration(EntityTypeBuilder<OrderDb> builder) 
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.TotalPrice);
        }
    }
}
