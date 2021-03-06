using ApplicationFMS.Interfaces;
using CoreFMS.Entities;
using InfrastructureFMSDB.Configurations;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureFMSDB
{
    public class FMSDataContext : DbContext, IFMSDataContext
    {
        public FMSDataContext(DbContextOptions<FMSDataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FMSDataContext).Assembly);
        }
        public DbSet<City> City { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Education> Education { get; set; }
        public DbSet<Feedback> Feedback { get; set; }
        public DbSet<FeedbackSubType> FeedbackSubType { get; set; }
        public DbSet<FeedbackType> FeedbackType { get; set; }
        public DbSet<OperationMode> OperationMode { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ReactionFeedback> ReactionFeedback { get; set; }
        public DbSet<ReactionComment> ReactionComment { get; set; }
        public DbSet<Reply> Reply { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Sector> Sector { get; set; }
        public DbSet<Share> Share { get; set; }
        public DbSet<SocialMedium> SocialMedia { get; set; }
        public DbSet<CoreFMS.Entities.System> System { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Log> Log { get; set; }
    }

}