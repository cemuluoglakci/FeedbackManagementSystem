using System;
using System.Collections.Generic;

namespace CoreFMS.Entities
{
    public partial class Comment
    {
        public Comment()
        {
            InverseCommentNavigation = new HashSet<Comment>();
            Reactions = new HashSet<Reaction>();
        }

        public int Id { get; set; }
        public int FeedbackId { get; set; }
        public int UserId { get; set; }
        public int? CommentId { get; set; }
        public string Text { get; set; } = null!;
        public int IsAnonym { get; set; }
        public int IsActive { get; set; }
        public int IsChecked { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Comment? CommentNavigation { get; set; }
        public virtual Feedback Feedback { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Comment> InverseCommentNavigation { get; set; }
        public virtual ICollection<Reaction> Reactions { get; set; }
    }
}
