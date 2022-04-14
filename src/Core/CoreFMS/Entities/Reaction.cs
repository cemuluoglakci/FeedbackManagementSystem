using System;
using System.Collections.Generic;

namespace CoreFMS.Entities
{
    public partial class Reaction
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? FeedbackId { get; set; }
        public int? CommentId { get; set; }
        public int Sentiment { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; } = true;
        public virtual Comment? Comment { get; set; }
        public virtual Feedback? Feedback { get; set; }
        public virtual User User { get; set; } = null!;
    }
}
