using CoreFMS.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfrastructureFMSDB.Configurations
{
    public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> entity)
        {
            entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

            entity.HasIndex(e => e.CompanyId, "fk_feedback_company");

            entity.HasIndex(e => e.SubTypeId, "fk_feedback_feedbackSubType");

            entity.HasIndex(e => e.TypeId, "fk_feedback_feedbackType");

            entity.HasIndex(e => e.ProductId, "fk_feedback_product");

            entity.HasIndex(e => e.SectorId, "fk_feedback_sector");

            entity.HasIndex(e => e.UserId, "fk_feedback_user");

            entity.HasIndex(e => e.DirectedToEmployeeId, "fk_feedback_companyEmployee");

            entity.Property(e => e.Id).HasColumnType("int(11)");

            entity.Property(e => e.CompanyId).HasColumnType("int(11)");

            entity.Property(e => e.DislikeCount).HasColumnType("int(11)");

            entity.Property(e => e.IsActive)
                .HasColumnType("int(1)")
                .HasDefaultValueSql("'1'");

            entity.Property(e => e.IsAnonym).HasColumnType("int(1)");

            entity.Property(e => e.IsChecked).HasColumnType("int(1)");

            entity.Property(e => e.IsReplied).HasColumnType("int(1)");

            entity.Property(e => e.IsSolved).HasColumnType("int(1)");

            entity.Property(e => e.LikeCount).HasColumnType("int(11)");

            entity.Property(e => e.ProductId).HasColumnType("int(11)");

            entity.Property(e => e.SectorId).HasColumnType("int(5)");

            entity.Property(e => e.Shared).HasColumnType("int(11)");

            entity.Property(e => e.SubTypeId).HasColumnType("int(5)");

            entity.Property(e => e.Text).HasMaxLength(400);

            entity.Property(e => e.Title).HasMaxLength(45);

            entity.Property(e => e.TypeId).HasColumnType("int(5)");

            entity.Property(e => e.UserId).HasColumnType("int(11)");

            entity.Property(e => e.DirectedToEmployeeId).HasColumnType("int(11)");

            entity.HasOne(d => d.Company)
                .WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("fk_feedback_company");

            entity.HasOne(d => d.Product)
                .WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("fk_feedback_product");

            entity.HasOne(d => d.Sector)
                .WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.SectorId)
                .HasConstraintName("fk_feedback_sector");

            entity.HasOne(d => d.SubType)
                .WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.SubTypeId)
                .HasConstraintName("fk_feedback_feedbackSubType");

            entity.HasOne(d => d.Type)
                .WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_feedback_feedbackType");

            entity.HasOne(d => d.User)
                .WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_feedback_user");

            entity.HasOne(d => d.DirectedCompanyEmployee)
                .WithMany(p => p.DirectedFeedbacks)
                .HasForeignKey(d => d.DirectedToEmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_feedback_companyEmployee");
        }


    }
}
