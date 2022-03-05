using infrastructure.database.model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace infrastructure.database.configurations
{
    public class ProductConfiguration
    {
        public ProductConfiguration(EntityTypeBuilder<ProductDb> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                 .ValueGeneratedOnAdd();
            builder.Property(x => x.Price);
            builder.Property(x => x.CurrencyId)
                .IsRequired();
            builder.Property(x => x.ProductTypeId)
                .IsRequired();
        }
    }
}
