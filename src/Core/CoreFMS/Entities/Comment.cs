using System;
using System.Collections.Generic;

namespace CoreFMS.Entities
{
    public partial class Comment
    {
        public Comment()
        {
            ChildComment = new HashSet<Comment>();
            Reactions = new HashSet<ReactionComment>();
        }

        public int Id { get; set; }
        public int FeedbackId { get; set; }
        public int UserId { get; set; }
        public int? ParentCommentId { get; set; }
        public string Text { get; set; } = null!;
        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }
        public bool IsAnonym { get; set; }
        public bool IsActive { get; set; }
        public bool IsChecked { get; set; }
        public DateTime CreatedAt { get; set; }

        //public virtual Comment? CommentNavigation { get; set; }
        public virtual Comment? ParentComment { get; set; }
        public virtual Feedback Feedback { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        //public virtual ICollection<Comment> InverseCommentNavigation { get; set; }
        public virtual ICollection<Comment> ChildComment { get; set; }
        public virtual ICollection<ReactionComment> Reactions { get; set; }
    }
}
