using System;
using System.Collections.Generic;

namespace CoreFMS.Entities
{
    public partial class User
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            Feedbacks = new HashSet<Feedback>();
            Reactions = new HashSet<ReactionFeedback>();
            Replies = new HashSet<Reply>();
            Shares = new HashSet<Share>();
        }

        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string Salt { get; set; } = null!;
        public string Hash { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? RegisteredAt { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public DateTime? LastFailedLoginAt { get; set; }
        public int? FailedLoginAttemptCount { get; set; }
        public int? CityId { get; set; }
        public int? EducationId { get; set; }
        public int RoleId { get; set; }
        public int? CompanyId { get; set; }
        public bool IsActive { get; set; }
        public bool IsTwoFactorAuth { get; set; }

        public virtual City? City { get; set; }
        public virtual Company? Company { get; set; }
        public virtual Education? Education { get; set; }
        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<ReactionFeedback> Reactions { get; set; }
        public virtual ICollection<Reply> Replies { get; set; }
        public virtual ICollection<Share> Shares { get; set; }
    }
}
