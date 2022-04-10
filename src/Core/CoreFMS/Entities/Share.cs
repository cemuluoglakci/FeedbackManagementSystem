using System;
using System.Collections.Generic;

namespace CoreFMS.Entities
{
    public partial class Share
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FeedbackId { get; set; }
        public int SocialMediaId { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Feedback Feedback { get; set; } = null!;
        public virtual SocialMedium SocialMedia { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
