using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CoreFMS.Entities;

namespace InfrastructureFMSDB.Configurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.Property(e => e.Id).HasColumnName("Id");

            builder.Property(e => e.CountryName)
                .IsRequired();

        }
    }

}
