using System;
using System.Collections.Generic;

namespace CoreFMS.Entities
{
    public partial class Reply
    {
        public int Id { get; set; }
        public int FeedbackId { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; } = null!;
        public bool IsActive { get; set; }
        public bool IsChecked { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Feedback Feedback { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
