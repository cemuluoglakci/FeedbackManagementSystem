using System;

namespace CoreFMS.Entities
{
    public partial class ReactionComment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CommentId { get; set; }
        public bool Sentiment { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; } = true;
        public virtual Comment? Comment { get; set; }
        public virtual User User { get; set; } = null!;
    }
}
