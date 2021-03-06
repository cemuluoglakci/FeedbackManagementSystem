using System.Collections.Generic;

namespace CoreFMS.Entities
{
    public partial class FeedbackSubType
    {
        public FeedbackSubType()
        {
            Feedbacks = new HashSet<Feedback>();
        }

        public int Id { get; set; }
        public string SubTypeName { get; set; } = null!;
        public bool IsActive { get; set; } = true;

        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
