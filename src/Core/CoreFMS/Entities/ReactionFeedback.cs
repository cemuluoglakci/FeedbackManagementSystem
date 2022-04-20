using System;

namespace CoreFMS.Entities
{
    public partial class ReactionFeedback
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FeedbackId { get; set; }
        public bool Sentiment { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; } = true;
        public virtual Feedback? Feedback { get; set; }
        public virtual User User { get; set; } = null!;
    }
}
