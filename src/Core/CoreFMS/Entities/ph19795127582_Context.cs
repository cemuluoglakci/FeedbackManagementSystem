using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CoreFMS.Entities
{
    public partial class ph19795127582_Context : DbContext
    {
        public ph19795127582_Context()
        {
        }

        public ph19795127582_Context(DbContextOptions<ph19795127582_Context> options)
            : base(options)
        {
        }

        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Company> Companies { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<Education> Educations { get; set; } = null!;
        public virtual DbSet<Feedback> Feedbacks { get; set; } = null!;
        public virtual DbSet<FeedbackCombined> FeedbackCombineds { get; set; } = null!;
        public virtual DbSet<FeedbackSubType> FeedbackSubTypes { get; set; } = null!;
        public virtual DbSet<FeedbackType> FeedbackTypes { get; set; } = null!;
        public virtual DbSet<LocationCombined> LocationCombineds { get; set; } = null!;
        public virtual DbSet<OperationMode> OperationModes { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Reaction> Reactions { get; set; } = null!;
        public virtual DbSet<Reply> Replies { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Sector> Sectors { get; set; } = null!;
        public virtual DbSet<Share> Shares { get; set; } = null!;
        public virtual DbSet<SocialMedium> SocialMedia { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserCombined> UserCombineds { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=n1nlmysql63plsk.secureserver.net;uid=fmsdbadmin;pwd=afn35M1#;database=ph19795127582_;persistsecurityinfo=True", Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.5.51-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8_general_ci")
                .HasCharSet("utf8");

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("City");

                entity.HasIndex(e => e.CountryId, "fk_city_country");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.CityName).HasMaxLength(50);

                entity.Property(e => e.CountryId).HasColumnType("int(5)");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("fk_city_country");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.CommentId, "fk_comment_comment");

                entity.HasIndex(e => e.FeedbackId, "fk_comment_feedback");

                entity.HasIndex(e => e.UserId, "fk_comment_user");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.CommentId).HasColumnType("int(11)");

                entity.Property(e => e.FeedbackId).HasColumnType("int(11)");

                entity.Property(e => e.IsActive)
                    .HasColumnType("int(1)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.IsAnonym).HasColumnType("int(1)");

                entity.Property(e => e.IsChecked).HasColumnType("int(1)");

                entity.Property(e => e.Text).HasMaxLength(400);

                entity.Property(e => e.UserId).HasColumnType("int(11)");

                entity.HasOne(d => d.CommentNavigation)
                    .WithMany(p => p.InverseCommentNavigation)
                    .HasForeignKey(d => d.CommentId)
                    .HasConstraintName("fk_comment_comment");

                entity.HasOne(d => d.Feedback)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.FeedbackId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_comment_feedback");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_comment_user");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("Company");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.SectorId, "fk_company_sector");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.CompanyName).HasMaxLength(45);

                entity.Property(e => e.SectorId).HasColumnType("int(11)");

                entity.HasOne(d => d.Sector)
                    .WithMany(p => p.Companies)
                    .HasForeignKey(d => d.SectorId)
                    .HasConstraintName("fk_company_sector");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("Country");

                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.CountryName).HasMaxLength(80);

                entity.Property(e => e.Iso)
                    .HasMaxLength(2)
                    .IsFixedLength();

                entity.Property(e => e.Iso3)
                    .HasMaxLength(3)
                    .IsFixedLength();

                entity.Property(e => e.Nicename).HasMaxLength(80);

                entity.Property(e => e.Numcode).HasColumnType("smallint(6)");

                entity.Property(e => e.Phonecode).HasColumnType("int(5)");
            });

            modelBuilder.Entity<Education>(entity =>
            {
                entity.ToTable("Education");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(5)");

                entity.Property(e => e.EducationName).HasMaxLength(45);
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.ToTable("Feedback");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.CompanyId, "fk_feedback_company");

                entity.HasIndex(e => e.SubTypeId, "fk_feedback_feedbackSubType");

                entity.HasIndex(e => e.TypeId, "fk_feedback_feedbackType");

                entity.HasIndex(e => e.ProductId, "fk_feedback_product");

                entity.HasIndex(e => e.SectorId, "fk_feedback_sector");

                entity.HasIndex(e => e.UserId, "fk_feedback_user");

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
            });

            modelBuilder.Entity<FeedbackCombined>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("FeedbackCombined");

                entity.Property(e => e.CompanyId).HasColumnType("int(11)");

                entity.Property(e => e.CompanyName).HasMaxLength(45);

                entity.Property(e => e.DislikeCount).HasColumnType("int(11)");

                entity.Property(e => e.Email).HasMaxLength(45);

                entity.Property(e => e.FirstName).HasMaxLength(45);

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.IsActive)
                    .HasColumnType("int(1)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.IsAnonym).HasColumnType("int(1)");

                entity.Property(e => e.IsChecked).HasColumnType("int(1)");

                entity.Property(e => e.IsReplied).HasColumnType("int(1)");

                entity.Property(e => e.IsSolved).HasColumnType("int(1)");

                entity.Property(e => e.LastName).HasMaxLength(45);

                entity.Property(e => e.LikeCount).HasColumnType("int(11)");

                entity.Property(e => e.Phone).HasMaxLength(45);

                entity.Property(e => e.ProductId).HasColumnType("int(11)");

                entity.Property(e => e.ProductName).HasMaxLength(45);

                entity.Property(e => e.SectorId).HasColumnType("int(5)");

                entity.Property(e => e.SectorName).HasMaxLength(45);

                entity.Property(e => e.Shared).HasColumnType("int(11)");

                entity.Property(e => e.SubTypeId).HasColumnType("int(5)");

                entity.Property(e => e.SubTypeName).HasMaxLength(45);

                entity.Property(e => e.Text).HasMaxLength(400);

                entity.Property(e => e.Title).HasMaxLength(45);

                entity.Property(e => e.TypeId).HasColumnType("int(5)");

                entity.Property(e => e.TypeName).HasMaxLength(45);

                entity.Property(e => e.UserId).HasColumnType("int(11)");
            });

            modelBuilder.Entity<FeedbackSubType>(entity =>
            {
                entity.ToTable("FeedbackSubType");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.SubTypeName).HasMaxLength(45);
            });

            modelBuilder.Entity<FeedbackType>(entity =>
            {
                entity.ToTable("FeedbackType");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.TypeName).HasMaxLength(45);
            });

            modelBuilder.Entity<LocationCombined>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("LocationCombined");

                entity.Property(e => e.CityName).HasMaxLength(50);

                entity.Property(e => e.CountryName)
                    .HasMaxLength(80)
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Iso)
                    .HasMaxLength(2)
                    .IsFixedLength()
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.Iso3)
                    .HasMaxLength(3)
                    .IsFixedLength()
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.Numcode).HasColumnType("smallint(6)");

                entity.Property(e => e.Phonecode).HasColumnType("int(5)");
            });

            modelBuilder.Entity<OperationMode>(entity =>
            {
                entity.ToTable("OperationMode");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.ModeName).HasMaxLength(45);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.CompanyId, "fk_product_company");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.CompanyId).HasColumnType("int(11)");

                entity.Property(e => e.ProductName).HasMaxLength(45);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("fk_product_company");
            });

            modelBuilder.Entity<Reaction>(entity =>
            {
                entity.ToTable("Reaction");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.CommentId, "fk_reaction_comment");

                entity.HasIndex(e => e.FeedbackId, "fk_reaction_feedback");

                entity.HasIndex(e => e.UserId, "fk_reaction_user");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.CommentId).HasColumnType("int(11)");

                entity.Property(e => e.FeedbackId).HasColumnType("int(11)");

                entity.Property(e => e.Sentiment).HasColumnType("int(1)");

                entity.Property(e => e.UserId).HasColumnType("int(11)");

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.Reactions)
                    .HasForeignKey(d => d.CommentId)
                    .HasConstraintName("fk_reaction_comment");

                entity.HasOne(d => d.Feedback)
                    .WithMany(p => p.Reactions)
                    .HasForeignKey(d => d.FeedbackId)
                    .HasConstraintName("fk_reaction_feedback");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reactions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_reaction_user");
            });

            modelBuilder.Entity<Reply>(entity =>
            {
                entity.ToTable("Reply");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.FeedbackId, "fk_reply_feedback");

                entity.HasIndex(e => e.UserId, "fk_reply_user");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.FeedbackId).HasColumnType("int(11)");

                entity.Property(e => e.IsActive)
                    .HasColumnType("int(1)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.IsChecked).HasColumnType("int(1)");

                entity.Property(e => e.Text).HasMaxLength(400);

                entity.Property(e => e.UserId).HasColumnType("int(11)");

                entity.HasOne(d => d.Feedback)
                    .WithMany(p => p.Replies)
                    .HasForeignKey(d => d.FeedbackId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_reply_feedback");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Replies)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_reply_user");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.RoleName).HasMaxLength(45);
            });

            modelBuilder.Entity<Sector>(entity =>
            {
                entity.ToTable("Sector");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.SectorName).HasMaxLength(45);
            });

            modelBuilder.Entity<Share>(entity =>
            {
                entity.ToTable("Share");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.FeedbackId, "fk_share_feedback");

                entity.HasIndex(e => e.SocialMediaId, "fk_share_socialMedia");

                entity.HasIndex(e => e.UserId, "fk_share_user");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.FeedbackId).HasColumnType("int(11)");

                entity.Property(e => e.SocialMediaId).HasColumnType("int(11)");

                entity.Property(e => e.UserId).HasColumnType("int(11)");

                entity.HasOne(d => d.Feedback)
                    .WithMany(p => p.Shares)
                    .HasForeignKey(d => d.FeedbackId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_share_feedback");

                entity.HasOne(d => d.SocialMedia)
                    .WithMany(p => p.Shares)
                    .HasForeignKey(d => d.SocialMediaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_share_socialMedia");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Shares)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_share_user");
            });

            modelBuilder.Entity<SocialMedium>(entity =>
            {
                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.SocialMediaName).HasMaxLength(45);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

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
            });

            modelBuilder.Entity<UserCombined>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("UserCombined");

                entity.Property(e => e.CityId).HasColumnType("int(11)");

                entity.Property(e => e.CityName).HasMaxLength(50);

                entity.Property(e => e.CompanyId).HasColumnType("int(11)");

                entity.Property(e => e.CompanyName).HasMaxLength(45);

                entity.Property(e => e.CountryName)
                    .HasMaxLength(80)
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.EducationId).HasColumnType("int(11)");

                entity.Property(e => e.EducationName).HasMaxLength(45);

                entity.Property(e => e.Email).HasMaxLength(45);

                entity.Property(e => e.FailedLoginAttemptCount).HasColumnType("int(3)");

                entity.Property(e => e.FirstName).HasMaxLength(45);

                entity.Property(e => e.Hash).HasMaxLength(45);

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Iso)
                    .HasMaxLength(2)
                    .IsFixedLength()
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.Iso3)
                    .HasMaxLength(3)
                    .IsFixedLength()
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.LastName).HasMaxLength(45);

                entity.Property(e => e.Numcode).HasColumnType("smallint(6)");

                entity.Property(e => e.Phone).HasMaxLength(45);

                entity.Property(e => e.Phonecode).HasColumnType("int(5)");

                entity.Property(e => e.RegisteredAt).HasColumnType("timestamp");

                entity.Property(e => e.RoleId)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'2'");

                entity.Property(e => e.RoleName).HasMaxLength(45);

                entity.Property(e => e.Salt).HasMaxLength(45);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
