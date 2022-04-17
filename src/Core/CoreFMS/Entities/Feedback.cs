using System;
using System.Collections.Generic;

namespace CoreFMS.Entities
{
    public partial class Feedback
    {
        public Feedback()
        {
            Comments = new HashSet<Comment>();
            Reactions = new HashSet<Reaction>();
            Replies = new HashSet<Reply>();
            Shares = new HashSet<Share>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = null!;
        public string Text { get; set; } = null!;
        public int? SectorId { get; set; }
        public int? CompanyId { get; set; }
        public int? ProductId { get; set; }
        public int TypeId { get; set; }
        public int? SubTypeId { get; set; }
        public int? Shared { get; set; }
        public int? LikeCount { get; set; }
        public int? DislikeCount { get; set; }
        public bool IsAnonym { get; set; }
        public bool IsActive { get; set; }
        public bool IsChecked { get; set; }
        public bool IsReplied { get; set; }
        public bool IsSolved { get; set; }
        public bool IsArchived { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? DirectedToEmploteeId { get; set; }

        public virtual Company? Company { get; set; }
        public virtual Product? Product { get; set; }
        public virtual Sector? Sector { get; set; }
        public virtual FeedbackSubType? SubType { get; set; }
        public virtual FeedbackType Type { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Reaction> Reactions { get; set; }
        public virtual ICollection<Reply> Replies { get; set; }
        public virtual ICollection<Share> Shares { get; set; }
    }
}
