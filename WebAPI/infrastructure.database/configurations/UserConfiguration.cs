using infrastructure.database.model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace infrastructure.database.configurations
{
    public class UserConfiguration
    {
        public UserConfiguration(EntityTypeBuilder<UserDb> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name);
            builder.Property(x => x.Password);
        }
    }
}
