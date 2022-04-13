using CoreFMS.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureFMSDB.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> entity)
        {
            entity.HasIndex(e => e.Id, "Id_UNIQUE")
    .IsUnique();

            entity.Property(e => e.Id).HasColumnType("int(11)");

            entity.Property(e => e.RoleName).HasMaxLength(45);


        }
    }
}
