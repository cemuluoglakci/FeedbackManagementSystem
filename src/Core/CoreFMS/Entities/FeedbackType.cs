using System.Collections.Generic;

namespace CoreFMS.Entities
{
    public partial class FeedbackType
    {
        public FeedbackType()
        {
            Feedbacks = new HashSet<Feedback>();
        }

        public int Id { get; set; }
        public string TypeName { get; set; } = null!;

        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
