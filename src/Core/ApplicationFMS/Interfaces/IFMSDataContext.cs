using CoreFMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;

namespace ApplicationFMS.Interfaces
{
    public interface IFMSDataContext
    {

        DbSet<City> City { get; set; }
        DbSet<Comment> Comment { get; set; }
        DbSet<Company> Company { get; set; }
        DbSet<Country> Country { get; set; }
        DbSet<Education> Education { get; set; }
        DbSet<Feedback> Feedback { get; set; }
        DbSet<FeedbackSubType> FeedbackSubType { get; set; }
        DbSet<FeedbackType> FeedbackType { get; set; }
        DbSet<OperationMode> OperationMode { get; set; }
        DbSet<Product> Product { get; set; }
        DbSet<ReactionFeedback> ReactionFeedback { get; set; }
        DbSet<ReactionComment> ReactionComment { get; set; }
        DbSet<Reply> Reply { get; set; }
        DbSet<Role> Role { get; set; }
        DbSet<Sector> Sector { get; set; }
        DbSet<Share> Share { get; set; }
        DbSet<SocialMedium> SocialMedia { get; set; }
        DbSet<CoreFMS.Entities.System> System { get; set; }
        DbSet<User> User { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
