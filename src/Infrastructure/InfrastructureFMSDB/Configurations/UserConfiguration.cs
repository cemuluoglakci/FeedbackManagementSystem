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
    public  class UserConfiguration : IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.HasIndex(e => e.Email, "Email_UNIQUE")
                    .IsUnique();

            entity.HasIndex(e => e.Id, "Id_UNIQUE")
                .IsUnique();

            entity.HasIndex(e => e.RoleId, "RoleId");

            entity.HasIndex(e => e.CityId, "fk_user_city");

            entity.HasIndex(e => e.CompanyId, "fk_user_company");

            entity.HasIndex(e => e.EducationId, "fk_user_education");

            entity.Property(e => e.Id).HasColumnType("int(11)");

            entity.Property(e => e.CityId).HasColumnType("int(11)");

            entity.Property(e => e.CompanyId).HasColumnType("int(11)");

            entity.Property(e => e.EducationId).HasColumnType("int(11)");

            entity.Property(e => e.Email).HasMaxLength(45);

            entity.Property(e => e.FailedLoginAttemptCount).HasColumnType("int(3)");

            entity.Property(e => e.FirstName).HasMaxLength(45);

            entity.Property(e => e.Hash).HasMaxLength(45);

            entity.Property(e => e.IsActive)
                .HasColumnType("int(1)")
                .HasDefaultValueSql("'1'");

            entity.Property(e => e.IsTwoFactorAuth).HasColumnType("int(1)");

            entity.Property(e => e.LastName).HasMaxLength(45);

            entity.Property(e => e.Phone).HasMaxLength(45);

            entity.Property(e => e.RegisteredAt)
                .HasColumnType("timestamp")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(e => e.RoleId)
                .HasColumnType("int(11)")
                .HasDefaultValueSql("'2'");

            entity.Property(e => e.Salt).HasMaxLength(45);

            entity.HasOne(d => d.City)
                .WithMany(p => p.Users)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("fk_user_city");

            entity.HasOne(d => d.Company)
                .WithMany(p => p.Users)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("fk_user_company");

            entity.HasOne(d => d.Education)
                .WithMany(p => p.Users)
                .HasForeignKey(d => d.EducationId)
                .HasConstraintName("fk_user_education");

            entity.HasOne(d => d.Role)
                .WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_role");



        }

    }
}
